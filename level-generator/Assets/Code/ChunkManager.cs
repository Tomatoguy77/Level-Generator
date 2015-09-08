using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkManager : MonoBehaviour
{
    [Header("Chunk prefabs")]
    public List<GameObject> m_chunks = new List<GameObject>();

    //screen width in game unit
    private float m_screenWidthGameUnits;
    private List<GameObject> m_chunksClones = new List<GameObject>();
         
    void Awake()
    {
        this.m_screenWidthGameUnits = this.getHalfScreenWidth();
    }

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            m_chunksClones.Add(getRandomChunk(Vector3.zero));
        }

        sortChunks(m_chunksClones);

        //normale functie met 2 parameters
        plus(1, 1);//2
        //Hier onder is een functie die een int returned zie functie
        int returnInt = plusReturn(2, 2);//4
        //print naar console
        Debug.Log(returnInt);

        //maak alle chunks aan
        //sorteer de nieuwe chunk zo dat ze het scherm volledig vullen
    }

    void Update()
    {
        foreach (var chunk in m_chunksClones)
        {
            moveChunk(chunk, 3f);
            
        }
        for (int i = 0; i < m_chunksClones.Count; i++)
        {
            if (checkBoundsChunk(m_chunksClones[i]))
            {
                Object.Destroy(m_chunksClones[i]);
                m_chunksClones.Remove(m_chunksClones[i]);
                m_chunksClones.Add(getRandomChunk(Vector3.zero));
                sortChunks(m_chunksClones);



            }
        }
      
       
      

        //check of alle chunks nog binne scherm zijn
        
        

        
        //delete de chunks die buiten het scherm zijn
        //beweeg alle chunks
       
       // moveChunk(GameObject.Find("Chunk1(Clone)"),1f);
    //    moveChunk(GameObject.Find("Chunk2(Clone)"), 1f);
   //     moveChunk(GameObject.Find("Chunk3(Clone)"), 1f);

    }

    //_a + _b functie
    //deze functie returned void. Dus niets.
    //Hier heb je 2 parameters _a en _b.
    //deze moeten ingevuld worden als je de functie aan roept.
    private void plus(int _a, int _b)
    {
        Debug.Log(_a + _b);
    }

    //_a + _b functie
    //deze functie returned een int!
    //return betekend dat je iets terug krijgt uit de functie.
    //in dit geval is het een int.
    private int plusReturn(int _a, int _b)
    {
        int antwoord = _a + _b;
        return antwoord;
    }

    /// <summary>
    /// Sorteer de chunk achter elkaar
    /// </summary>
    /// <param name="_chunks">List van chunks die gesorteerd moeten worden</param>
    private void sortChunks(List<GameObject> _chunks)
    {
        if (_chunks.Count < 1)
        {
            Debug.Log("Error sort chunk! list heeft geen elementen");
            return;
        }
        //get first chunk position
        var l_firstChunkV3 = _chunks[0].transform.position;
        //sort chunks
        for (int i = 0; i < _chunks.Count; i++)
        {
            _chunks[i].transform.position = new Vector3(l_firstChunkV3.x + (getChunkWidth(_chunks[i]) * i), 0);
        }
    }

    /// <summary>
    /// Check of de chunk aan de linker kant uit het scherm is
    /// </summary>
    /// <param name="_chunk">chunk die we gaan checken</param>
    /// <returns>True = uit scherm, False = binnen scherm</returns>
    private bool checkBoundsChunk(GameObject _chunk)
    {
        if (_chunk.transform.position.x < 0 - (m_screenWidthGameUnits + getChunkWidth(_chunk) / 2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Beweeg een chunk
    /// </summary>
    /// <param name="_chunk">Chunk dat bewongen moet worden</param>
    /// <param name="_speed">Snelheid van bewegen</param>
    private void moveChunk(GameObject _chunk, float _speed)
    {
        _chunk.transform.position -= new Vector3(_speed * Time.deltaTime, 0);
    }

    /// <summary>
    /// Haal een random chunk op
    /// </summary>
    /// <param name="_position">positie van game object</param>
    /// <returns>chunk clone gameobject</returns>
    private GameObject getRandomChunk(Vector3 _position)
    {
        return spawnChunk(m_chunks[Random.Range(0, m_chunks.Count)], _position);
    }

    /// <summary>
    /// Spawn een chunk
    /// </summary>
    /// <param name="_chunk">chunk game object</param>
    /// <param name="_position">position van game object</param>
    /// <returns>chunk clone</returns>
    private GameObject spawnChunk(GameObject _chunk, Vector3 _position)
    {
        return (GameObject)Instantiate(_chunk, _position, Quaternion.identity);
    }

    /// <summary>
    /// Haal de breete op van de chunk
    /// </summary>
    /// <param name="_chunk">chunk game object</param>
    /// <returns>breete in float</returns>
    private float getChunkWidth(GameObject _chunk)
    {
        return _chunk.GetComponent<BoxCollider2D>().size.x;
    }

    /// <summary>
    /// Haal de helft van de schermbreete op in game units
    /// </summary>
    /// <returns>game with in game units</returns>
    private float getHalfScreenWidth()
    {
        //orthoWidth = orthographicSize / screenHeight * screenWidth;
        return (Camera.main.orthographicSize / Camera.main.pixelHeight * Camera.main.pixelWidth);
    }
}
