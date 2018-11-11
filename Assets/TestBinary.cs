using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.UI;
using System.Text;

public class Record{
    // 浮点数是否等于零
    public static bool IsZero(float num){
        return num >= -float.Epsilon && num <= float.Epsilon;
    }

    public float first;
    public float second;
    public float third;
    public float fourth;
    public float fifth;
    public float sixth;
    public float seventh;
    public float eighth;

    public override string ToString(){
        //return string.Format("{0} {1} {2} {3} {4} {5} {6} {7}",
                             //first.ToString("F4"),
                             //second.ToString("F4"),
                             //third.ToString("F4"),
                             //fourth.ToString("F4"),
                             //fifth.ToString("F4"),
                             //sixth.ToString("F4"),
                             //seventh.ToString("F4"),
                             //eighth.ToString("F4"));

        return string.Format("{0} {1} {2} {3} {4} {5} {6} {7}",
        first,
        second,
        third,
        fourth,
        fifth,
        sixth,
        seventh,
        eighth);
    }
}

public class Node {
    public Record[] m_kNode = null;

    public Node(){
        m_kNode = new Record[70];
    }

    public override string ToString(){
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < m_kNode.Length; ++i){
            builder.Append(m_kNode[i].ToString());

            if (i < m_kNode.Length - 1)
                builder.Append("|");
        }

        return builder.ToString();
    }
}

public class TestBinary : MonoBehaviour {
    string m_strProgress = "";
    float m_fProgress = 0;

    public Progress m_objProgress = null;

    const string intPutPath = "./Assets/Z005F002.txt";
    const string outputPath = "./Assets/saved.txt";

    Node[] m_kAllData = null;
	// Use this for initialization
	void Start () {
	}

    IEnumerator StreamData(){
        FileStream fs;
        fs = new FileStream(intPutPath, FileMode.OpenOrCreate, FileAccess.Read);
        BinaryReader br = new BinaryReader(fs);

        int index = 0;
        int length = (int)br.BaseStream.Length;
        // 一行为40个字节，包括换行
        int recordNum = length / (32 * 70);

        Debug.LogFormat("文件流长度为{0}字节", length);
        Debug.LogFormat("总记录数{0}", recordNum);




        m_kAllData = new Node[recordNum];

        while (br.BaseStream.Position != br.BaseStream.Length)
        {
            m_kAllData[index] = new Node();

            Debug.LogErrorFormat("{0}, {1}, {2}", index, br.BaseStream.Position, br.BaseStream.Length);

            for (int i = 0; i < m_kAllData[index].m_kNode.Length; ++i){
                m_kAllData[index].m_kNode[i] = new Record();

                float data = br.ReadSingle();
                m_kAllData[index].m_kNode[i].first = Record.IsZero(data) ? 0f : data;
                data = br.ReadSingle();
                m_kAllData[index].m_kNode[i].second = Record.IsZero(data) ? 0f : data;
                data = br.ReadSingle();
                m_kAllData[index].m_kNode[i].third = Record.IsZero(data) ? 0f : data;
                data = br.ReadSingle();
                m_kAllData[index].m_kNode[i].fourth = Record.IsZero(data) ? 0f : data;
                data = br.ReadSingle();
                m_kAllData[index].m_kNode[i].fifth = Record.IsZero(data) ? 0f : data;
                data = br.ReadSingle();
                m_kAllData[index].m_kNode[i].sixth = Record.IsZero(data) ? 0f : data;
                data = br.ReadSingle();
                m_kAllData[index].m_kNode[i].seventh = Record.IsZero(data) ? 0f : data;
                data = br.ReadSingle();
                m_kAllData[index].m_kNode[i].eighth = Record.IsZero(data) ? 0f : data;
            }


            index++;
            //Debug.LogFormat("记录{0}: {1},{2},{3},{4}", pos, first.ToString("F4"),
            //sec.ToString("F4"),
            //third.ToString("F4"),
            //fourth.ToString("F4"));

            if (index % 100 == 0){
                yield return new WaitForEndOfFrame();
            }

            m_fProgress = (float)index / recordNum;    

            m_strProgress = string.Format("进度{0}/{1}, {2}", 
                                          index,
                                          recordNum,
                                          string.Format("{0:N0}%", m_fProgress * 100f));

            m_objProgress.SetFillAmound(m_fProgress);
        }

        fs.Close();
        yield return null;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0,
                                0,
                                100,
                                30), "open"))
        {
            ReadFile();
        }



        if (GUI.Button(new Rect(Screen.width - 100,
                        0,
                        100,
                        30), "save"))
        {
            WriteFile();
        }



        GUI.Label(new Rect(Screen.width - 200, Screen.height - 100, 200, 100), m_strProgress);
    }

    void ReadFile(){
        StartCoroutine(StreamData());
    }

    void WriteFile(){
        // 二进制写
        //FileStream fs;
        //fs = new FileStream(output, FileMode.Create, FileAccess.Write);

        //BinaryWriter bw = new BinaryWriter(fs);

        //for (int i = 0; i < m_kAllData.Length; ++i){
        //    bw.Write(m_kAllData[i].first);
        //    bw.Write('\0');
        //    bw.Write(m_kAllData[i].second);
        //    bw.Write('\0');
        //    bw.Write(m_kAllData[i].third);
        //    bw.Write('\0');
        //    bw.Write(m_kAllData[i].fourth);
        //    bw.Write('\0');
        //    bw.Write(m_kAllData[i].fifth);
        //    bw.Write('\0');
        //    bw.Write(m_kAllData[i].sixth);
        //    bw.Write('\0');
        //    bw.Write(m_kAllData[i].seventh);
        //    bw.Write('\0');
        //    bw.Write(m_kAllData[i].eighth);

        //    if (i != m_kAllData.Length - 1)
        //        bw.Write('\n');
        //}

        if (File.Exists(outputPath)){
            File.Delete(outputPath);
        }

        // 文本文件写
        using (StreamWriter sw = new StreamWriter(outputPath))
        {
            for (int i = 0; i < m_kAllData.Length; ++i){
                Debug.LogFormat("写第{0}行", i);
                sw.WriteLine(m_kAllData[i].ToString());
            }
        }
    }
}
