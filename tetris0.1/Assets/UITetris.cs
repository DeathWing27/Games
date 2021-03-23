using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITetris : MonoBehaviour {

    public GameObject blockPfb;
    public GameObject[,] blocks;
    Tetris tet;

	// Use this for initialization
	void Start () {
		
        tet = this.GetComponent<Tetris>();

        blocks = new GameObject[16,8];

        for (int y = 0; y < 16; y++) {
            for (int x = 0; x < 8; x++) {
                blocks[y,x] = GameObject.Instantiate( blockPfb );
                blocks[y,x].transform.position = new Vector3( x, 15-y, 0 );
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
		
        for (int y = 0; y < 16; y++) {
            for (int x = 0; x < 8; x++) {
                if ( tet.pole[y,x] > 0 ){
                    blocks[y,x].SetActive( true );
                    if ( tet.pole[y,x] == 1 ){
                        blocks[y,x].GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    else
                    {
                        blocks[y,x].GetComponent<SpriteRenderer>().color = Color.white;    
                    }

                }
                else{
                    blocks[y,x].SetActive( false );
                    blocks[y,x].GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }

	}

}
