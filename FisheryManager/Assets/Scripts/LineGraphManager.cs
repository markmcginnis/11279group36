using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LineGraphManager : MonoBehaviour
{

	public GameObject linerenderer;
	public GameObject pointer;

	public GameObject pointerRed;
	public GameObject pointerBlue;

	public GameObject HolderPrefb;

	public GameObject holder;
	public GameObject xLineNumber;

	public Material bluemat;
	public Material greenmat;

	public Text topValue;

	public List<GraphData> graphData1 = new List<GraphData>();
	public List<GraphData> graphData2 = new List<GraphData>();

	private GraphData gd;
	private float highestValue = 100;

	public Transform origin;
	public Transform origin2;

	public TextMesh player1name;
	public TextMesh player2name;

	private float lrWidth = 0.1f;
	private int dataGap = 0;

	public FisheryManager fm;
	public EconomyManager em;

	public class GraphData
	{
		public float dataVal;
	}

	int mode = 0;

	void Start()
	{
		// showing graph
		ShowGraph();
	}

	public void ShiftLeftData1()
	{
		for (int i = 1; i < graphData1.Count; i++)
		{
			graphData1[i - 1] = graphData1[i];
		}
		graphData1.RemoveAt(9);
	}

	public void ShiftLeftData2()
	{
		for (int i = 1; i < graphData2.Count; i++)
		{
			graphData2[i - 1] = graphData2[i];
		}
		graphData2.RemoveAt(9);
	}

	public void ShowData(GraphData[] gdlist, int playerNum, float gap)
	{

		// Adjusting value to fit in graph
		for (int i = 0; i < gdlist.Length; i++)
		{
			// since Y axis is from 0 to 7 we are dividing the dataVal with the highestValue
			// so that we get a value less than or equals to 1 and than we can multiply that
			// number with Y axis range to fit in graph. 
			// e.g. dataVal = 90, highest = 90 so 90/90 = 1 and than 1*7 = 7 so for 90, Y = 7
			gdlist[i].dataVal = (gdlist[i].dataVal / highestValue) * 7;
		}
		if (playerNum == 1)
			StartCoroutine(BarGraphBlue(gdlist, gap));
		else if (playerNum == 2)
			StartCoroutine(BarGraphGreen(gdlist, gap));
	}

	public void AddPlayer1Data()
	{
		GameObject fisheryM = GameObject.Find("FisheryManager");
		fm = fisheryM.GetComponent<FisheryManager>();
		
		GraphData gd = new GraphData();
		gd.dataVal = fm.totalPopulation;
		if (graphData1.Count == 10)
        {
			ShiftLeftData1();
		}
		graphData1.Add(gd);
	}
	public void AddPlayer2Data()
	{
		GameObject economyM = GameObject.Find("EconomyManager");
		em = economyM.GetComponent<EconomyManager>();
		GraphData gd = new GraphData();
		gd.dataVal = em.totalRevenue/1000;
		if (graphData2.Count == 10)
		{
			ShiftLeftData2();
		}
		graphData2.Add(gd);
	}

	public void ShowGraph()
	{

		ClearGraph();

		if (graphData1.Count >= 1 && graphData2.Count >= 1)
		{
			holder = Instantiate(HolderPrefb, Vector3.zero, Quaternion.identity) as GameObject;
			holder.name = "h2";

			GraphData[] gd1 = new GraphData[graphData1.Count];
			GraphData[] gd2 = new GraphData[graphData2.Count];
			for (int i = 0; i < graphData1.Count; i++)
			{
				GraphData gd = new GraphData();
				gd.dataVal = graphData1[i].dataVal;
				gd1[i] = gd;
			}
			for (int i = 0; i < graphData2.Count; i++)
			{
				GraphData gd = new GraphData();
				gd.dataVal = graphData2[i].dataVal;
				gd2[i] = gd;
			}

			dataGap = GetDataGap(graphData2.Count);


			int dataCount = 0;
			int gapLength = 1;
			float gap = 1.0f;
			bool flag = false;

			while (dataCount < graphData2.Count)
			{
				if (dataGap > 1)
				{

					if ((dataCount + dataGap) == graphData2.Count)
					{

						dataCount += dataGap - 1;
						flag = true;
					}
					else if ((dataCount + dataGap) > graphData2.Count && !flag)
					{

						dataCount = graphData2.Count - 1;
						flag = true;
					}
					else
					{
						dataCount += dataGap;
						if (dataCount == (graphData2.Count - 1))
							flag = true;
					}
				}
				else
					dataCount += dataGap;

				gapLength++;
			}

			if (graphData2.Count > 13)
			{
				if (graphData2.Count < 40)
					gap = 13.0f / graphData2.Count;
				else if (graphData2.Count >= 40)
				{
					gap = 13.0f / gapLength;
				}
			}

			ShowData(gd1, 1, gap);
			ShowData(gd2,2,gap);
		}
	}

	public void ClearGraph()
	{
		if (holder)
			Destroy(holder);
	}

	int GetDataGap(int dataCount)
	{
		int value = 1;
		int num = 0;
		while ((dataCount - (40 + num)) >= 0)
		{
			value += 1;
			num += 20;
		}

		return value;
	}


	IEnumerator BarGraphBlue(GraphData[] gd, float gap)
	{
		float xIncrement = gap;
		int dataCount = 0;
		bool flag = false;
		Vector3 startpoint = new Vector3((origin.position.x + xIncrement), (origin.position.y + gd[dataCount].dataVal), (origin.position.z));//origin.position;//

		while (dataCount < gd.Length)
		{

			Vector3 endpoint = new Vector3((origin.position.x + xIncrement), (origin.position.y + gd[dataCount].dataVal), (origin.position.z));
			startpoint = new Vector3(startpoint.x, startpoint.y, origin.position.z);
			// pointer is an empty gameObject, i made a prefab of it and attach it in the inspector
			GameObject p = Instantiate(pointer, new Vector3(startpoint.x, startpoint.y, origin.position.z), Quaternion.identity) as GameObject;
			p.transform.parent = holder.transform;


			GameObject lineNumber = Instantiate(xLineNumber, new Vector3(origin.position.x + xIncrement, origin.position.y - 0.18f, origin.position.z), Quaternion.identity) as GameObject;
			lineNumber.transform.parent = holder.transform;
			lineNumber.GetComponent<TextMesh>().text = (dataCount + 1).ToString();


			// linerenderer is an empty gameObject with Line Renderer Component Attach to it, 
			// i made a prefab of it and attach it in the inspector
			GameObject lineObj = Instantiate(linerenderer, startpoint, Quaternion.identity) as GameObject;
			lineObj.transform.parent = holder.transform;
			lineObj.name = dataCount.ToString();

			LineRenderer lineRenderer = lineObj.GetComponent<LineRenderer>();

			lineRenderer.material = bluemat;
			lineRenderer.SetWidth(lrWidth, lrWidth);
			lineRenderer.SetVertexCount(2);

			while (Vector3.Distance(p.transform.position, endpoint) > 0.2f)
			{
				float step = 5 * Time.deltaTime;
				p.transform.position = Vector3.MoveTowards(p.transform.position, endpoint, step);
				lineRenderer.SetPosition(0, startpoint);
				lineRenderer.SetPosition(1, p.transform.position);

				yield return null;
			}

			lineRenderer.SetPosition(0, startpoint);
			lineRenderer.SetPosition(1, endpoint);


			p.transform.position = endpoint;
			GameObject pointered = Instantiate(pointerRed, endpoint, pointerRed.transform.rotation) as GameObject;
			pointered.transform.parent = holder.transform;
			startpoint = endpoint;

			if (dataGap > 1)
			{
				if ((dataCount + dataGap) == gd.Length)
				{
					dataCount += dataGap - 1;
					flag = true;
				}
				else if ((dataCount + dataGap) > gd.Length && !flag)
				{
					dataCount = gd.Length - 1;
					flag = true;
				}
				else
				{
					dataCount += dataGap;
					if (dataCount == (gd.Length - 1))
						flag = true;
				}
			}
			else
				dataCount += dataGap;

			xIncrement += gap;

			yield return null;

		}
	}

	IEnumerator BarGraphGreen(GraphData[] gd, float gap)
	{
		float xIncrement = gap;
		int dataCount = 0;
		bool flag = false;

		Vector3 startpoint = new Vector3((origin2.position.x + xIncrement), (origin2.position.y + gd[dataCount].dataVal), (origin2.position.z));
		while (dataCount < gd.Length)
		{

			Vector3 endpoint = new Vector3((origin2.position.x + xIncrement), (origin2.position.y + gd[dataCount].dataVal), (origin2.position.z));
			startpoint = new Vector3(startpoint.x, startpoint.y, origin2.position.z);
			// pointer is an empty gameObject, i made a prefab of it and attach it in the inspector
			GameObject p = Instantiate(pointer, new Vector3(startpoint.x, startpoint.y, origin2.position.z), Quaternion.identity) as GameObject;
			p.transform.parent = holder.transform;

			// linerenderer is an empty gameObject with Line Renderer Component Attach to it, 
			// i made a prefab of it and attach it in the inspector
			GameObject lineObj = Instantiate(linerenderer, startpoint, Quaternion.identity) as GameObject;
			lineObj.transform.parent = holder.transform;
			lineObj.name = dataCount.ToString();

			LineRenderer lineRenderer = lineObj.GetComponent<LineRenderer>();

			lineRenderer.material = greenmat;
			lineRenderer.SetWidth(lrWidth, lrWidth);
			lineRenderer.SetVertexCount(2);

			while (Vector3.Distance(p.transform.position, endpoint) > 0.2f)
			{
				float step = 5 * Time.deltaTime;
				p.transform.position = Vector3.MoveTowards(p.transform.position, endpoint, step);
				lineRenderer.SetPosition(0, startpoint);
				lineRenderer.SetPosition(1, p.transform.position);

				yield return null;
			}

			lineRenderer.SetPosition(0, startpoint);
			lineRenderer.SetPosition(1, endpoint);


			p.transform.position = endpoint;
			GameObject pointerblue = Instantiate(pointerBlue, endpoint, pointerBlue.transform.rotation) as GameObject;
			pointerblue.transform.parent = holder.transform;
			startpoint = endpoint;

			if (dataGap > 1)
			{
				if ((dataCount + dataGap) == gd.Length)
				{
					dataCount += dataGap - 1;
					flag = true;
				}
				else if ((dataCount + dataGap) > gd.Length && !flag)
				{
					dataCount = gd.Length - 1;
					flag = true;
				}
				else
				{
					dataCount += dataGap;
					if (dataCount == (gd.Length - 1))
						flag = true;
				}
			}
			else
				dataCount += dataGap;

			xIncrement += gap;

			yield return null;

		}
	}
}
