using UnityEngine;

public class RookMove : MoveController
{
    protected override void Start()
    {
        base.Start();
        if (Coord.y == (Rule.Colour + 8) % 9)
        {
            if (Coord.x == 0)
            {
                Game.Kings[tag].CastleRooks[0] = this;
            }
            else if (Coord.x == 7)
            {
                Game.Kings[tag].CastleRooks[1] = this;
            }
        }
    }
}
