using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour {
  //source: https://github.com/SebLague/Pathfinding/tree/master/Episode%2003%20-%20astar
	public Transform seeker, target;
	Grid grid;

	void Awake() {
		grid = GetComponent<Grid> ();
	}

	void Update() {
		FindPath (seeker.position, target.position);
	}

	void FindPath(Vector3 startPos, Vector3 targetPos) {
		Node startNode = grid.NodeFromWorldPoint(startPos);
		Node targetNode = grid.NodeFromWorldPoint(targetPos);

		List<Node> openSet = new List<Node>(); //the set of nodes to be evaluated
		HashSet<Node> closedSet = new HashSet<Node>(); //nodes already evaluated
		openSet.Add(startNode); //add starting node to the open set

		while (openSet.Count > 0) {
			Node node = openSet[0]; //current node is the first node
			for (int i = 1; i < openSet.Count; i ++) {
				if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost) { //if the node in the open set has an f cost less than the current node, then the current node is equal to the that node in the open set
					if (openSet[i].hCost < node.hCost)
						node = openSet[i];
				}
			}

			openSet.Remove(node); //now that we have the node from the open set with the lowest f cost, remove it from the open set and ad it to the closed set
			closedSet.Add(node);

			if (node == targetNode) {// if we found our target, return
				RetracePath(startNode,targetNode);
				return;
			}

			foreach (Node neighbour in grid.GetNeighbours(node)) { //for each neighbor of the current node
				if (!neighbour.walkable || closedSet.Contains(neighbour)) { //if neighbor is not traversable or neighbor is in closed skip to the next neighbor
					continue;
				}

				int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
				if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) { //if new path to neighbor is shorter or neighbor is not in open
					neighbour.gCost = newCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, targetNode); //set f cost of neighbor
					neighbour.parent = node; //set parent of neighbor to current

					if (!openSet.Contains(neighbour))//if neighbor is not in open set
						openSet.Add(neighbour); //add neighbor to open set
				}
			}
		}
	}

	void RetracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse();

		grid.path = path;

	}

	int GetDistance(Node nodeA, Node nodeB) {
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (dstX > dstY)
			return 14*dstY + 10* (dstX-dstY);
		return 14*dstX + 10 * (dstY-dstX);
	}
}