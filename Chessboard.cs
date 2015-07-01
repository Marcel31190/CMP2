using UnityEngine;
using System.Collections;

public class Chessboard : MonoBehaviour {

	public int m_iSize = 10;
	public int m_Range = 1;
	GameObject[,] m_Grid;
	int[,] m_State;

	// Use this for initialization
	void Start () {

		m_Grid = new GameObject[m_iSize, m_iSize];
		m_State = new int[m_iSize, m_iSize];

		for (int i = 0; i < m_iSize; i++)
		for (int j = 0; j < m_iSize; j++) {
			GameObject kachel = GameObject.CreatePrimitive(PrimitiveType.Quad);
			kachel.name = "Kachel ["+i+"]["+j+"]";
			m_Grid[i, j] = kachel;
			kachel.transform.position = new Vector3 (i, j, 0);
			kachel.transform.parent = this.transform;
			} 

		Camera.main.transform.position = new Vector3 (m_iSize/2, m_iSize/2, -100);
		Camera.main.orthographicSize = m_iSize;

		transform.position = new Vector3 (0.5f, 0.5f, 0);

		for (int i = 0; i < m_iSize; i++)
			for (int j = 0; j < m_iSize; j++) {
			float farben = Random.value;
			    if (farben <= 0.5)
			    m_Grid [i, j].GetComponent<Renderer>().material.color = Color.blue;
			    else
				m_Grid [i, j].GetComponent<Renderer>().material.color = Color.white;
			}
	}
	
	int GetAliveNeighbours (int _iColumn, int _iRow){
		int AliveNeigboursCounter = 0;
		for (int i = -m_Range; i <= m_Range; i++) {
			int iTmp = _iColumn + i;
			for (int j = -m_Range; j <= m_Range; j++) {
				int jTmp = _iRow + j;
				if (!(iTmp == _iColumn && jTmp == _iRow) && !(iTmp < 0) && !(jTmp < 0) && (iTmp < m_iSize) && (jTmp < m_iSize))
				if (m_Grid [iTmp, jTmp].GetComponent<Renderer>().material.color == Color.blue)
					AliveNeigboursCounter ++;
				}
			}
		return AliveNeigboursCounter;
	}

	void KillAll(){
		for (int i = 0; i < m_iSize; i++)
		for (int j = 0; j < m_iSize; j++) {
				m_Grid [i, j].GetComponent<Renderer>().material.color = Color.white;
		}
	}

	void Toggle (int _iCol, int _iRow){
		if (m_Grid [_iCol, _iRow].GetComponent<Renderer>().material.color == Color.white)
		{
				m_Grid [_iCol, _iRow].GetComponent<Renderer>().material.color = Color.blue;
		}
	    else
		{
				m_Grid [_iCol, _iRow].GetComponent<Renderer>().material.color = Color.white;
		}
	}

	void ToggleMouseField(){
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		int xIndex = (int)mouseWorldPos.x;
		int yIndex = (int)mouseWorldPos.y;
		Toggle (xIndex, yIndex);
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.K))
			KillAll();
		
		if (Input.GetMouseButtonDown (0))
			ToggleMouseField ();
			//Toggle (2, 2);
			/*for (int i = 0; i < m_iSize; i++)
				for (int j = 0; j < m_iSize; j++) {
					Toggle (i, j);
				}*/

		if (Input.GetKeyDown (KeyCode.Space) == false)
		return;

		for (int iCol = 0; iCol < m_iSize; iCol++) {
			for (int iRow = 0; iRow < m_iSize; iRow++) {
				int iNeighalive = GetAliveNeighbours(iCol, iRow);
					m_State [iCol, iRow] = iNeighalive;
			}
		}

 		for (int iCol = 0; iCol < m_iSize; iCol++) {
			for (int iRow = 0; iRow < m_iSize; iRow++) {
 
				if (m_Grid [iCol, iRow].GetComponent<Renderer>().material.color == Color.white)
				{
					if (m_State[iCol, iRow] == 3)
						m_Grid [iCol, iRow].GetComponent<Renderer>().material.color = Color.blue;
				}
				else
				{
					if (m_State[iCol, iRow] > 3 || m_State[iCol, iRow] < 2)
						m_Grid [iCol, iRow].GetComponent<Renderer>().material.color = Color.white;
				}	
			}
		}

		Debug.Log (GetAliveNeighbours (4, 4));
	}
}
