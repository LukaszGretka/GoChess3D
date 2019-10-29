using Assets._Scripts.Board.Abstract;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour, ISquare
{
    public char Rank => 'A';

    public char File => '1';

    public bool IsOccupied => false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
