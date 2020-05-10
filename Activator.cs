using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class Activator : MonoBehaviour
{
    public Tilemap ActivatorMap;
    public Tilemap ActivatorMapSolid;

    public Tile RedD;
    public Tile RedA;

    public Tile GreenD;
    public Tile GreenA;

    public Tile BlueD;
    public Tile BlueA;

    public Tile YellowD;
    public Tile YellowA;

    public int sizeX;
    public int sizeY;

    List<Vector3Int> Red;
    List<Vector3Int> Green;
    List<Vector3Int> Blue;
    List<Vector3Int> Yellow;

    public bool RedAct;
    public bool GreenAct;
    public bool BlueAct;
    public bool YellowAct;

    public bool RedStart;
    public bool GreenStart;
    public bool BlueStart;
    public bool YellowStart;

    bool activating; //Au toggle, est-ce que on active ou désactive

    public void Start()
    {
        Red = new List<Vector3Int>();
        Green = new List<Vector3Int>();
        Blue = new List<Vector3Int>();
        Yellow = new List<Vector3Int>();

        Setup();
    }

    public void Setup()
    {
      for (int x = 0; x < sizeX; x ++) {
          for (int y = 0; y < sizeY; y ++) {
              var temp = ActivatorMap.GetTile(new Vector3Int(-x + sizeX / 2, -y + sizeY / 2, 0));
              if (temp == RedD || temp == RedA) {
                  Red.Add(new Vector3Int(-x + sizeX / 2, -y + sizeY / 2, 0));
              }
              if (temp == GreenD || temp == GreenA) {
                  Green.Add(new Vector3Int(-x + sizeX / 2, -y + sizeY / 2, 0));
              }
              if (temp == BlueD || temp == BlueA) {
                  Blue.Add(new Vector3Int(-x + sizeX / 2, -y + sizeY / 2, 0));
              }
              if (temp == YellowD || temp == YellowA) {
                  Yellow.Add(new Vector3Int(-x + sizeX / 2, -y + sizeY / 2, 0));
              }
          }
      }
      if (RedStart != RedAct) {
          ToggleColour(0);
      }
      if (GreenStart != GreenAct) {
          ToggleColour(1);
      }
      if (BlueStart != BlueStart) {
          ToggleColour(2);
      }
      if (YellowStart != YellowStart) {
          ToggleColour(3);
      }
    }


    public void ToggleColour(int color) { //1 : red, 2 : green, 3 : blue, 4 : yellow
        List<Vector3Int> tempList = null;
        Tile tempTile = null;
        if (color == 0) {
            tempList = Red;
            if (RedAct) {
                tempTile = RedD;
                activating = false;
                RedAct = false;
            }
            else {
                tempTile = RedA;
                activating = true;
                RedAct = true;
            }
        }
        else if(color == 1) {
            tempList = Green;
            if (GreenAct) {
                tempTile = GreenD;
                activating = false;
                GreenAct = false;
            }
            else {
                tempTile = GreenA;
                activating = true;
                GreenAct = true;
            }
        }
        else if (color == 2) {
            tempList = Blue;
            if (BlueAct) {
                tempTile = BlueD;
                activating = false;
                BlueAct = false;
            }
            else {
                tempTile = BlueA;
                activating = true;
                BlueAct = true;
            }
        }
        else if (color == 3) {
            tempList = Yellow;
            if (YellowAct) {
                tempTile = YellowD;
                activating = false;
                YellowAct = false;
            }
            else {
                tempTile = YellowA;
                activating = true;
                YellowAct = true;
            }
        }

        foreach (Vector3Int vect in tempList) {
            if (activating) {
                ActivatorMapSolid.SetTile(vect, tempTile);
                ActivatorMap.SetTile(vect, null);
            }
            else {
                ActivatorMapSolid.SetTile(vect, null);
                ActivatorMap.SetTile(vect, tempTile);
            }
        }

    }
}
