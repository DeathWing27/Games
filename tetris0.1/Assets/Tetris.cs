using UnityEngine;
using System.Collections;

public class Tetris : MonoBehaviour {
	int xCord=3;
	int yCord=1;

    //16x8
    public int[,] pole = new int[,]{
        {0,0,0,0,0,0,0,0},
        {0,0,0,1,1,1,0,0},
        {0,0,0,0,1,0,0,0},
        {0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0},
        {2,0,0,0,0,0,0,2},
        {2,0,0,0,0,0,0,2},
        {2,0,0,0,0,0,0,2},
        {2,2,2,2,2,2,2,2},
    };
	
   
	void Start () {
    				

	}

    //двигаемся вправо
	void MoveRight(){
		int[,] tmp = new int[16,8];
		for (int y = 0; y < 16; y++) {
			for (int x = 0; x < 8; x++) {
				if ( x == 7 && pole[y,x] == 1){ 
					return;
				}
				if ( pole[y,x] == 1 && pole[y,x+1] == 2 ){
					return;
				}

				if ( pole[y,x] == 1 ){
					tmp[y,x+1] = 1;
				}
				if ( pole[y,x] == 2 ){
					tmp[y,x] = pole[y,x];
				}
			}
		}
		xCord++;
		pole = tmp;
		
	}

    //движение влево
	void MoveLeft(){
		
		int[,] tmp = new int[16,8];
		for (int y = 0; y < 16; y++) {
			for (int x = 7; x >= 0; x--) {
				if ( x == 0 && pole[y,x] == 1){ 
					return;
				}
				if ( pole[y,x]==1 && pole[y,x-1] == 2 ){
					return;
				}
				if ( pole[y,x] == 1 ){
					tmp[y,x-1] = 1;
				}

				if ( pole[y,x] == 2 ){
					tmp[y,x] = pole[y,x];
				}
			}
		}
		xCord--;
		pole = tmp;
		
	}

    //движение вниз
	void MoveDown(){
		
		int[,] tmp = new int[16,8];

		for (int y = 15; y >= 0; y--) {
			for (int x = 0; x < 8; x++) {
				if (y > 0) {
					if (pole [y, x] == 2 && pole [y - 1, x] == 1) {
						Replace (); //заменяем все единицы на двойки
						return; //преккащаем
					}
				}
				if (y == 15 && pole [y, x] == 1) { //добрались до низа  
					Replace (); //заменяем все единицы на двойки
					return; //преккащаем
				}   

				if (y < 15) {
					if (pole [y, x] == 1) {
						tmp [y + 1, x] = 1;
					}
				}

				if (pole [y, x] == 2) {
					tmp [y, x] = 2;
				}
			}
		}


		yCord++;
		pole = tmp;



	}
	void Replace () {
		for (int y = 0; y < 16; y++) {
			for (int x = 0; x < 8; x++) {    
				if ( pole[y,x] >0 ){ //если не 0
					pole[y,x] = 2; //записываем 2
				}
			}
		}


	}
	void Rotate () {
		
		int[,] tmp = new int[3, 3];
	
		for (int y = yCord; y < yCord + 3; y++) {
			for (int x = xCord; x < xCord + 3; x++) {
				tmp [y - yCord, x - xCord] = pole [y, x];
				if (pole [y, x] == 2) {
					return;
				}
			}
		}
		for (int y = 0; y < 3; y++) {
			for (int x = 0; x < 3; x++) {
				pole [y + yCord, x + xCord] = tmp [2 - x, y];
			}
		}
	}
	void CleanLine( int line )
		{
			for (int x=0; x<8; x++) {
				pole[line,x] = 0;
			}
		}





					



	public virtual void Update(){	

		if ( Input.GetKeyDown("s")){
			MoveDown();
		}

		if ( Input.GetKeyDown("d")){
			MoveRight();
		}

		if ( Input.GetKeyDown("a")){
			MoveLeft();
		}
		if (Input.GetKeyDown("space")) {
			Rotate ();
		}
		//if (Input.GetKeyDown("c")) {
		//	CleanLine (); 
		//}
		   		
	}
}


