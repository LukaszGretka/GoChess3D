using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField]
    private char _column;

    [SerializeField]
    public char _row;

    public bool IsOccupied { get; set; }

    internal void SetCoordinates(char row, char column)
    {
        _row = row;
        _column = column;
    }

    internal void SetColor(Material material)
    {
        gameObject.GetComponent<Renderer>().material = material;
    }
}
