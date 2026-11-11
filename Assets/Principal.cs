﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Analytics;


public class Principal : MonoBehaviour { 
	public static bool isInteractable = true;
	public static int nivelActual = 0;
	//public static string urlBase = "https://campus.mineduc.asso.com.ar/wsmartenautas/index.php";
	public static string urlBase = "https://campuseducativo.santafe.gob.ar/herramientas/wsmartenautas/index.php";
    static string fileNameDat = "avance05.dat"; 
	static string fromScene = 	"Main_Menu";
    public static string nombreActNumerosPrimos = "Numeros Primos";
    public static string nombreActCuestionarios = "Cuestionario";
    public static string nombreActEcuaciones    = "Ecuaciones";
    public static string nombreActOperacionesEnteros    = "Operaciones con numeros enteros";
    public static string nombreActLabEstadosMateria = "Estado de la materia";
    public static string nombreActLabEstrucutraAtomica = "Estructura Atomica";
    public static string nombreActSimRaSolar    = "Explora Sistema Solar";
    public static string nombreActSimVuelo      = "Simulador Vuelo";
    public static string nombreActSimAterrisaje = "Simulador Aterrizaje";
	public static string nombreActSimExplorador = "Simulador Explorador";
	 
    public static string nombreRango1Geonauta   = "Geonauta";
    public static string nombreRango2Aeronauta  = "Aeronauta";
    public static string nombreRango3Cosmonauta = "Cosmonauta";
    public static string nombreRango4Martenauta = "Martenauta";

	public static string nombreInsigniaNumerosPrimos     = "Numeros Primos";
	public static string nombreInsigniaEcuaciones        = "Ecuaciones";
	public static string nombreInsigniaLaboratorios      = "Laboratorios";
	public static string nombreInsigniaCuestionarios     = "Cuestionarios";
	public static string nombreInsigniaOperacionesEnteros= "Operaciones con numeros enteros";

    public static int avanceNoIniciada = 0;
    public static int avanceIniciada   = 1;
    public static int avanceCompleta   = 2;



    public static string user=null;


    public static Color colorOK = new Color(98f/255f, 169f/255f, 160f/255f,1f);
    public static Color colorERR= new Color(229f / 255f, 111f / 255f, 111f / 255f, 1f);

    public static Juego juego;
     
    public static Avance avance;


	public static string Pretty(string texto){
		if (Principal.nombreActSimExplorador.ToUpper().Equals(texto.ToUpper())) {
			return "EXPLORADOR DE MARTE";
		} else
			return texto.ToUpper ();
	}
		
	public static void Log(string msg){
		if (Debug.isDebugBuild) Debug.Log(msg);

	}
   // public Text ui_puntaje;

	public void iraLoginOMenu(){
		if (Principal.avance.mantenerLogin ) {
			if (Principal.avance.ultimoUsuario != null && Principal.avance.ultimoUsuario.Length > 0) {
				Principal.Log ("iraLoginOMenu" + Principal.avance.ultimoUsuario);
				this.cambiarUsuarioPorNombre (Principal.avance.ultimoUsuario);
			}
			else {
				this.cargarEscena ("Login");
			}
		} else {
		
			this.cargarEscena ("Login");
		}


	
	}


	public void cambiarUsuario  (GameObject usuarioTexto,GameObject usuarioNotNull) {
		
		InputField text = usuarioTexto.GetComponent<InputField>();


		if (text.text == null || text.text.Trim ().Length == 0) {
			usuarioNotNull.SetActive (true);

		} else {

				guardarAvance ();

				Principal.user = text.text;
		        

		        juego = null;
		        avance = null;
		        cargarModelo();

				if (Principal.avance.avanceJugadorActual ().token == null  ) {
					this.cargarEscena ("Main_Menu");
				} else {
					//Principal.user = user;
					this.cargarEscena ("Login_clave");
				}
		}
    }

	public void cambiarUsuarioPorNombre(string nombreUsuario) {
		guardarAvance ();
		if (nombreUsuario == null || nombreUsuario.Length == 0)
		{
			user = "default";
		}
		else {
			user = nombreUsuario;
		}

		juego = null;
		avance = null;
		cargarModelo();
		Principal.avance.ultimoUsuario = user;
		this.cargarEscena("Main_Menu");
	}

    public string getActiveSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
    public void home (){
		SceneManager.LoadScene ("PrincipalScene");  
	}

    public static void inicializar()
    {
        
        if (juego == null)
        {
            Principal p = new Principal();
            p.cargarModelo();
        }

    }
    

    public void salir(){
		Application.Quit();
	}
	  

	public void volver(){
		if (fromScene == null)
			home ();
		else
			cargarEscena (fromScene);
	}

	public void cargarAjustesAditiva(){
		 SceneManager.LoadScene ("AjustesAditiva",LoadSceneMode.Additive);  
	}

	public void cargarEscenaAditiva(string scena){
		SceneManager.LoadScene (scena,LoadSceneMode.Additive);  
	} 

	public void cerrarScenaAditiva(string nombre){
		SceneManager.UnloadSceneAsync (nombre);  
	}


	public void cerrarScenaAditiva(){
		SceneManager.UnloadSceneAsync (SceneManager.GetActiveScene().name);  
	}

	public string escenaActualNombre(){
		return SceneManager.GetActiveScene().name;
	}
	public void cargarEscena(string name){
		fromScene=SceneManager.GetActiveScene().name;
        guardarAvance();
		SceneManager.LoadScene (name);  
	}

	public void cargarCuestionario(int nivel){
		LabTaller04Cuestionario.nivel = nivel;	
		Principal.nivelActual = nivel+1;

		SceneManager.LoadScene ("Cuestionario");  
	}

    public void cargarTallerEnteros(int nivel)
    {
        LabTaller02.nivel = nivel;
		Principal.nivelActual = nivel;
        SceneManager.LoadScene("Enteros");
    }
	public void cargarTallerPrimos(int nivel)
	{
		LabTaller01.nivel = nivel;
		Principal.nivelActual = nivel;
		SceneManager.LoadScene("Primos");
	}


    public void salirCuestionario(){
		switch(LabTaller04Cuestionario.nivel){
		case 0:
			SceneManager.LoadScene ("Desafio_01");  
			break;
		case 1:
			SceneManager.LoadScene ("Desafio_02");  
			break;
		case 2:
			SceneManager.LoadScene ("Desafio_03");  
			break;
		case 3:
			SceneManager.LoadScene ("Desafio_04");  
			break;
		case 4:
			SceneManager.LoadScene ("Desafio_06");  
			break;
		case 5:
			SceneManager.LoadScene ("Desafio_07");  
			break;
		case 6:
			SceneManager.LoadScene ("Desafio_08");  
			break;

		}
	}

	public void cargarEcuaciones(int nivel){
		LabTaller03.nivel = nivel;	
		Principal.nivelActual = nivel+1;
		SceneManager.LoadScene ("Ecuaciones");  
	}

	public void salirEcuaciones(){
		switch(LabTaller03.nivel){
		case 0:
			SceneManager.LoadScene ("Desafio_03");  
			break;
		case 1:
			SceneManager.LoadScene ("Desafio_05");  
			break;
		case 2:
			SceneManager.LoadScene ("Desafio_09");  
			break;

		}
	}

	public void registrarStat(string sceneName,string className,string meth){
		Analytics.SetUserId (Principal.user);

		AnalyticsEvent.Custom(sceneName, new Dictionary<string, object>
			{
				{ "class", className},
				{ "meth", meth},
				{ "usuario", Principal.user},
				{ "level", Principal.nivelActual},
				{ "time_elapsed", Time.timeSinceLevelLoad}
			});
	}

	// Use this for initialization
	void Start () {
  //      VuforiaBehaviour.Instance.enabled = false;
//        VuforiaRuntime.Instance.Deinit();
		Principal.isInteractable=true;
		if (juego == null) {
			cargarModelo ();
		}
        LabsVuforia vuforia = GetComponent<LabsVuforia>();
        if (vuforia == null) {
            LabsNoVuforia noVuforia = GetComponent<LabsNoVuforia>();
            if (noVuforia==null) gameObject.AddComponent<LabsNoVuforia>();
        }
		this.registrarStat (this.getActiveSceneName(),"Principal","Stat");
    }
	
	// Update is called once per frame
	void Update () {
		//Text puntaje = GameObject.Find ("puntaje").GetComponent<Text>();
		//ui_puntaje.text = "hola" ;
//		ui_puntaje.text	= ""+ juego.jugador.puntos;
	}

	public static void guardarAvanceStatic() {
		Principal p=new Principal();
		p.guardarAvance ();
	}

    public void guardarAvance() {
        if (user != null)
        {
            // string fileName = Application.persistentDataPath + "/" + "avance.dat";
            string fileName = Application.persistentDataPath + "/" + fileNameDat;
          //  Debug.Log("guardando avance.." + user + "->" + fileName);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(fileName);
            bf.Serialize(file, avance);
            file.Close();

           // Debug.Log("guardando avance..OK " + user + "->" + fileName);

        }
        else {
			Principal.Log("No hay datos de usuario para poder guardar"	+ Principal.user);
        }
        string json = JsonUtility.ToJson(avance.avanceJugadorActual(), true);
		Principal.Log("Guardando avance:"+json );
		Principal.Log("Guardando ultimo usuario:"+avance.ultimoUsuario + " usuario:" + Principal.user + " internet " + avance.internet + " mantenerlogin:" + avance.mantenerLogin );

        

    }

    public void iniciarAvance() {
   //     Debug.Log("iniciarAvance");
        string fileName = Application.persistentDataPath + "/" + fileNameDat;

        if (avance == null)
        {
			Principal.Log(fileName);
            if (File.Exists(fileName))
            {
                try
                {
  //                  Debug.Log("cargando..avance" + fileName);
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Open(fileName, FileMode.Open);
                    avance = (Avance)bf.Deserialize(file);
                    file.Close();
//                    Debug.Log("cargando..avance OK" + fileName);

                }
				catch(Exception e) 
                {
					Debug.Log (e,this);
					Principal.Log("error al crear avance.dat");
                }
            }
            else {
				Principal.Log("creando..avance" + fileName);
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(fileName);
                avance = new Avance();

                bf.Serialize(file, avance);
                file.Close();

				Principal.Log("creando..avance OK " + fileName);

            }
        }
		if (user==null ) 
			if(avance.ultimoUsuario != null)	user = avance.ultimoUsuario;
			else user = "default";
		string json = JsonUtility.ToJson(avance.avanceJugadorActual(), true);
		Principal.Log("inicializando  avance:" + json);
		Principal.Log("inicializando  avance usuario:"+avance.ultimoUsuario + " usuario:" + Principal.user + " internet " + avance.internet + " mantenerlogin:" + avance.mantenerLogin );

    }
    public void cargarModelo(){
        if (avance == null) {
            iniciarAvance();
        }
        if (juego == null)
        {
            iniciarModelo();
        }
        //string s = System.IO.File.ReadAllText("Assets\\modelo\\juego.txt");

       
    }

	public void setMetaDataNumerosPrimos(int level,int index,int numero,bool esprimo,int[] validos,int[] invalidos){

		//int[] invalidos = todos - validos;

		juego.taller01NumeroPrimoNivel[level].data[index] = new Taller01NumeroPrimoData();
		juego.taller01NumeroPrimoNivel[level].data[index].numero = numero;
		juego.taller01NumeroPrimoNivel[level].data[index].esPrimo = esprimo;
		juego.taller01NumeroPrimoNivel[level].data[index].validos =  validos;
		juego.taller01NumeroPrimoNivel[level].data[index].invalidos = invalidos;

	}

    public Juego iniciarModelo() {
        /*if (avance == null) {
            cargarModelo();
        }*/

//        Debug.Log("iniciarModelo");
        juego = new Juego();
        // juego.jugador = new Jugador();
        //juego.jugador.puntos = 10;
        //-----------------------------------------------------
		juego.taller01NumeroPrimoNivel = new Taller01NumeroPrimo[3];
		juego.taller01NumeroPrimoNivel[0]= new Taller01NumeroPrimo();
		juego.taller01NumeroPrimoNivel[1]= new Taller01NumeroPrimo();
		juego.taller01NumeroPrimoNivel[2]= new Taller01NumeroPrimo();
		juego.taller01NumeroPrimoNivel[0].data = new Taller01NumeroPrimoData[7];
        //-------------- 4[1, 2, 4]    { 3,6}
		juego.taller01NumeroPrimoNivel[0].data[0] = new Taller01NumeroPrimoData();
		juego.taller01NumeroPrimoNivel[0].data[0].numero = 4;
		juego.taller01NumeroPrimoNivel[0].data[0].esPrimo = false;
		juego.taller01NumeroPrimoNivel[0].data[0].validos = new int[] { 1, 2, 4 };
		juego.taller01NumeroPrimoNivel[0].data[0].invalidos = new int[] { 3, 6 };
        //-------------- 6[1, 2, 3, 6]  { 5,8}
		juego.taller01NumeroPrimoNivel[0].data[1] = new Taller01NumeroPrimoData();
		juego.taller01NumeroPrimoNivel[0].data[1].numero = 6;
		juego.taller01NumeroPrimoNivel[0].data[1].esPrimo = false;
		juego.taller01NumeroPrimoNivel[0].data[1].validos = new int[] { 1, 2, 3, 6 };
		juego.taller01NumeroPrimoNivel[0].data[1].invalidos = new int[] { 5, 8 };
        //-------------- 7[1, 7]  { 5,8}
		juego.taller01NumeroPrimoNivel[0].data[2] = new Taller01NumeroPrimoData();
		juego.taller01NumeroPrimoNivel[0].data[2].numero = 7;
		juego.taller01NumeroPrimoNivel[0].data[2].esPrimo = true;
		juego.taller01NumeroPrimoNivel[0].data[2].validos = new int[] { 1, 7 };
		juego.taller01NumeroPrimoNivel[0].data[2].invalidos = new int[] { 5, 8 };
        //------------------------- 8[1, 2, 4, 8]  { 3,6,9}
		juego.taller01NumeroPrimoNivel[0].data[3] = new Taller01NumeroPrimoData();
		juego.taller01NumeroPrimoNivel[0].data[3].numero = 8;
		juego.taller01NumeroPrimoNivel[0].data[3].esPrimo = false;
		juego.taller01NumeroPrimoNivel[0].data[3].validos = new int[] { 1, 2,4,8 };
		juego.taller01NumeroPrimoNivel[0].data[3].invalidos = new int[] { 3,6,9};
        //------------------------- 9[1, 3, 9]    { 2,7}
		juego.taller01NumeroPrimoNivel[0].data[4] = new Taller01NumeroPrimoData();
		juego.taller01NumeroPrimoNivel[0].data[4].numero = 9;
		juego.taller01NumeroPrimoNivel[0].data[4].esPrimo = false;
		juego.taller01NumeroPrimoNivel[0].data[4].validos = new int[] { 1, 3,9 };
		juego.taller01NumeroPrimoNivel[0].data[4].invalidos = new int[] { 2,7 };
        //------------------------- 10[1, 2, 5, 10] { 3,6,7}
		juego.taller01NumeroPrimoNivel[0].data[5] = new Taller01NumeroPrimoData();
		juego.taller01NumeroPrimoNivel[0].data[5].numero = 10;
		juego.taller01NumeroPrimoNivel[0].data[5].esPrimo = false;
		juego.taller01NumeroPrimoNivel[0].data[5].validos = new int[] { 1, 2, 5, 10 };
		juego.taller01NumeroPrimoNivel[0].data[5].invalidos = new int[] { 3, 6, 7 };
        //-------------------------
        //20[1, 2, 4, 5, 10, 20] { 8,40,-2,3}
		juego.taller01NumeroPrimoNivel[0].data[6] = new Taller01NumeroPrimoData();
		juego.taller01NumeroPrimoNivel[0].data[6].numero = 20;
		juego.taller01NumeroPrimoNivel[0].data[6].esPrimo = false;
		juego.taller01NumeroPrimoNivel[0].data[6].validos = new int[] { 1, 2, 4,5,10,20};
		juego.taller01NumeroPrimoNivel[0].data[6].invalidos = new int[] { 8,40,3};

		int puntaje = 0;
		foreach (Taller01NumeroPrimoData a in juego.taller01NumeroPrimoNivel[0].data)
        {
            puntaje += a.puntoOK;
        }
		juego.taller01NumeroPrimoNivel[0].puntajeMaximo = puntaje;
      /*----------------------------------------------------------------nivel 2 ----------------------------------*/

		int index=0;
		juego.taller01NumeroPrimoNivel[1].data = new Taller01NumeroPrimoData[12];

		// 17 [1,7,10,17]    {1,17}				
		setMetaDataNumerosPrimos(1,index++,17,true, new int[] { 1,17},new int[]  { 7,10});
		// 20  [1,2,4,5,10,20,]  {1,2,4,5,10,20}						
		setMetaDataNumerosPrimos(1,index++,20,false,new int[] { 1,2,4,5,10,20},new int[]  { 3,7,15,40});
		// 57  [1,3,5,7,19,57]  {1,3,19,57}    						
		setMetaDataNumerosPrimos(1,index++,57,false,new int[] { 1,3,19,57},new int[]  {5,7});
		// 61  [1,3,6,7,61]  {1,61}						
		setMetaDataNumerosPrimos(1,index++,61,true,new int[] { 1,61},new int[]  {3,6,7});
		// 13  [1,3,4,13]    {1,13}						
		setMetaDataNumerosPrimos(1,index++,13,true,new int[] { 1,13},new int[]  {3,4});
		// 50 [1,2,3,5,6,10,20,25,50] {1,2,5,10,25,50}						
		setMetaDataNumerosPrimos(1,index++,50,false,new int[] { 1,2,5,10,25,50},new int[]  {3,6,20});
		// 41 [1,2,4,5,21,41] {1,41}						
		setMetaDataNumerosPrimos(1,index++,41,true,new int[] { 1,41},new int[]  {2,4,5,21});
		// 29 [1,2,9,29] {1,29}						
		setMetaDataNumerosPrimos(1,index++,29,true,new int[] { 1,29},new int[]  {2,9});
		// 25  [1,3,4,5,7,25]    {1,5,25}						
		setMetaDataNumerosPrimos(1,index++,25,false,new int[] { 1,5,25},new int[]  {3,4,7});
		// 71 [1,2,3,5,6,7,9,11,20,71] {1,71}					
		setMetaDataNumerosPrimos(1,index++,71,true,new int[] { 1,71},new int[]  {2,3,5,6,7,9,11,20});

		// 99 [1,3,6,11,33,99] {1,3,9,11,99}						
		setMetaDataNumerosPrimos(1,index++,99,false,new int[] { 1,3,9,11,33,99},new int[]  {6});

		// 65 [1,2,5,13,15,35,65] {1,5,15,65}						
		setMetaDataNumerosPrimos(1,index++,65,false,new int[] { 1,5,13,65},new int[]  {2,15,35});

		puntaje = 0;
		foreach (Taller01NumeroPrimoData a in juego.taller01NumeroPrimoNivel[1].data)
		{
			puntaje += a.puntoOK;
		} 
		juego.taller01NumeroPrimoNivel[1].puntajeMaximo = puntaje;
		//  juego.taller01NumeroPrimoNivel1.puntajeRecord = avance.puntaje(user, "numerosPrimos", 1);

		/*----------------------------------------------------------------nivel 3 ----------------------------------*/

		index=0;
		juego.taller01NumeroPrimoNivel[2].data = new Taller01NumeroPrimoData[12];

		//102  {1,2,3,6,17,34,51,102}  [4,8] 						
		setMetaDataNumerosPrimos(2,index++,102,false,new int[] { 1,2,3,6,17,34,51,102},new int[]  { 4,8});
		//200  {1,2,4,5,8,10,20,25,40,50,100,200} [22,30,70]  						
		setMetaDataNumerosPrimos(2,index++,200,false,new int[] { 1,2,4,5,8,10,20,25,40,50,100,200},new int[]  { 22,30,70});
		//177  {1,3,59,177}[5,11]  
		setMetaDataNumerosPrimos(2,index++,177,false,new int[] { 1,3,59,177},new int[]  {5,11});
		//151  {1,151} [3,5,51]  
		setMetaDataNumerosPrimos(2,index++,151,true,new int[] { 1,151},new int[]  {3,5,51});
		//131 {1,131} [3,5,11]    
		setMetaDataNumerosPrimos(2,index++,131,true,new int[] { 1,131},new int[]  { 3,5,11});
		//150 {1,2,3,5,6,10,15,25,30,50,75,150}[12] 
		setMetaDataNumerosPrimos(2,index++,150,false,new int[] { 1,2,3,5,6,10,15,25,30,50,75,150},new int[]  { 12});
		//111 {1,3,37,111} [4,5,23] 
		setMetaDataNumerosPrimos(2,index++,111,false,new int[] {1,3,37,111},new int[]  { 4,5,23}); 
		//120 {1,2,3,4,5,6,8,10,12,15,20,24,30,40,60,120}[25] 
		setMetaDataNumerosPrimos(2,index++,120,false,new int[] { 1,2,3,4,5,6,8,10,12,15,20,24,30,40,60,120},new int[]  { 25});
		//117 {1,3,9,13,39,117} [7, 17]
		setMetaDataNumerosPrimos(2,index++,117,false,new int[] { 1,3,9,13,39,117},new int[]  { 7, 17});
		//100 {1,2,4,5,10,20,25,50,100}[] 
		setMetaDataNumerosPrimos(2,index++,100,false,new int[] { 1,2,4,5,10,20,25,50,100},new int[]  { });
		//101 {1,101}[2,3,41] 
		setMetaDataNumerosPrimos(2,index++,101,true,new int[] { 1,101},new int[]  { 2,3,41});
		//199 {1,199} [3,6,9,11,33,99] 
		setMetaDataNumerosPrimos(2,index++,199,true,new int[] { 1,199},new int[]  {3,6,9,11,33,99});


		puntaje = 0;
		foreach (Taller01NumeroPrimoData a in juego.taller01NumeroPrimoNivel[2].data)
		{
			puntaje += a.puntoOK;
		} 
		juego.taller01NumeroPrimoNivel[2].puntajeMaximo = puntaje;



        /*Numeros Naturales*/
        juego.taller02NumerosNaturales = new Taller02NumerosNaturales[3];
        juego.taller02NumerosNaturales[0] = new Taller02NumerosNaturales();
        juego.taller02NumerosNaturales[0].data = new Taller02NumerosNaturalesData[6];
        juego.taller02NumerosNaturales[0].data[0] = new Taller02NumerosNaturalesData();
        juego.taller02NumerosNaturales[0].data[1] = new Taller02NumerosNaturalesData();
        juego.taller02NumerosNaturales[0].data[2] = new Taller02NumerosNaturalesData();
        juego.taller02NumerosNaturales[0].data[3] = new Taller02NumerosNaturalesData();
        juego.taller02NumerosNaturales[0].data[4] = new Taller02NumerosNaturalesData();
        juego.taller02NumerosNaturales[0].data[5] = new Taller02NumerosNaturalesData();

        //(-9). (+3) = { -27, 12, -12, +27 ,-6}
        juego.taller02NumerosNaturales[0].data[0].operacion = "(-9) . (+3)";
        juego.taller02NumerosNaturales[0].data[0].resultados.Add("-27");
        juego.taller02NumerosNaturales[0].data[0].addOpciones(new int[] { -27, 12, -12, +27, -6 });

        //- 5 - 10 = { -15, +50, -5, -10, +9}
        juego.taller02NumerosNaturales[0].data[1].operacion = "- 5 - 10";
        juego.taller02NumerosNaturales[0].data[1].resultados.Add("-15");
        juego.taller02NumerosNaturales[0].data[1].addOpciones (new int[] { -15, +50, -5, -10, +9 });

        //(+8). (-2) = { -16, +6, +16, -10, -4}
        juego.taller02NumerosNaturales[0].data[2].operacion = "(+8) . (-2)";
        juego.taller02NumerosNaturales[0].data[2].resultados.Add("-16");
        juego.taller02NumerosNaturales[0].data[2].addOpciones (new int[] { -16, +6, +16, -10, -4 });

        //-23 + 15 = { -8 ,-38, +15,  +38, +8}
        juego.taller02NumerosNaturales[0].data[3].operacion = "-23 + 15";
        juego.taller02NumerosNaturales[0].data[3].resultados.Add("-8");
        juego.taller02NumerosNaturales[0].data[3].addOpciones(new int[] { -8, -38, +15, +38, +8 });

        //(-20) : (-10) = { -100, +100, +2, -2, +200}
        juego.taller02NumerosNaturales[0].data[4].operacion = "(-20) : (-10)";
        juego.taller02NumerosNaturales[0].data[4].resultados.Add("2");
        juego.taller02NumerosNaturales[0].data[4].addOpciones(new int[] { -100, +100, +2, -2, +200 });

        //-15 + 10 - 1 - 9 = { -15, 10, -45, 15, -5}
        juego.taller02NumerosNaturales[0].data[5].operacion = "-15 + 10 - 1 - 9";
        juego.taller02NumerosNaturales[0].data[5].resultados.Add("-15");
        juego.taller02NumerosNaturales[0].data[5].addOpciones(new int[]{ -15, 10, -45, 15, -5 });

         puntaje = 0;
        foreach (Taller02NumerosNaturalesData a in juego.taller02NumerosNaturales[0].data) {
            puntaje += a.puntoOK;
        }
        juego.taller02NumerosNaturales[0].puntajeMaximo = puntaje;

        //--------------------------------------------------------------------------

        juego.taller02NumerosNaturales[1] = new Taller02NumerosNaturales();
        juego.taller02NumerosNaturales[1].data = new Taller02NumerosNaturalesData[6];
        juego.taller02NumerosNaturales[1].data[0] = new Taller02NumerosNaturalesData();
        juego.taller02NumerosNaturales[1].data[1] = new Taller02NumerosNaturalesData();
        juego.taller02NumerosNaturales[1].data[2] = new Taller02NumerosNaturalesData();
        juego.taller02NumerosNaturales[1].data[3] = new Taller02NumerosNaturalesData();
        juego.taller02NumerosNaturales[1].data[4] = new Taller02NumerosNaturalesData();
        juego.taller02NumerosNaturales[1].data[5] = new Taller02NumerosNaturalesData();



        // -7 + 5 .3 = -6, 8, 6, -36
        juego.taller02NumerosNaturales[1].data[0].operacion = "-7 + 5 . 3";
        juego.taller02NumerosNaturales[1].data[0].resultados.Add("8");
        juego.taller02NumerosNaturales[1].data[0].addOpciones(new int[] { -6, 8, 6, -36 });

        // (-8). (-2) - 5 = 11, -21, 50, 56
        juego.taller02NumerosNaturales[1].data[1].operacion = "(-8) . (-2) - 5 ";
        juego.taller02NumerosNaturales[1].data[1].resultados.Add("11");
        juego.taller02NumerosNaturales[1].data[1].addOpciones(new int[] { 11, -21, 50, 56 });

        // (-10). (-10). (-10) = 1000, -1000, -30, 30
        juego.taller02NumerosNaturales[1].data[2].operacion = "(-10) . (-10) . (-10)";
        juego.taller02NumerosNaturales[1].data[2].resultados.Add("-1000");
        juego.taller02NumerosNaturales[1].data[2].addOpciones(new int[] { 1000, -1000, -30, 30 });

        // -10 - 10 - 10 = 1000, -1000, -30 , 30
        juego.taller02NumerosNaturales[1].data[3].operacion = "-10 - 10 - 10";
        juego.taller02NumerosNaturales[1].data[3].resultados.Add("-30");
        juego.taller02NumerosNaturales[1].data[3].addOpciones(new int[] { 1000, -1000, -30, 30 });

        // (-10) ∧ 2 = 100, -100, -20 , 20
        juego.taller02NumerosNaturales[1].data[4].operacion = "(-10) ^ 2";
        juego.taller02NumerosNaturales[1].data[4].resultados.Add("100");
        juego.taller02NumerosNaturales[1].data[4].addOpciones(new int[] { 100, -100, -20, 20 });

        // -7: -1 + 7: 1 = -7, -14, 14, 0
        juego.taller02NumerosNaturales[1].data[5].operacion = "-7 : -1 + 7 : 1";
        juego.taller02NumerosNaturales[1].data[5].resultados.Add("14");
        juego.taller02NumerosNaturales[1].data[5].addOpciones(new int[] { -7, -14, 14, 0 });

        puntaje = 0;
        foreach (Taller02NumerosNaturalesData a in juego.taller02NumerosNaturales[1].data)
        {
            puntaje += a.puntoOK; 
        }
        juego.taller02NumerosNaturales[1].puntajeMaximo = puntaje;

        
        //--------------------------------------------------------------------------

        juego.taller02NumerosNaturales[2] = new Taller02NumerosNaturales();
        juego.taller02NumerosNaturales[2].data = new Taller02NumerosNaturalesData[6];
        juego.taller02NumerosNaturales[2].data[0] = new Taller02NumerosNaturalesData();
        juego.taller02NumerosNaturales[2].data[1] = new Taller02NumerosNaturalesData();
        juego.taller02NumerosNaturales[2].data[2] = new Taller02NumerosNaturalesData();
        juego.taller02NumerosNaturales[2].data[3] = new Taller02NumerosNaturalesData();
        juego.taller02NumerosNaturales[2].data[4] = new Taller02NumerosNaturalesData();
        juego.taller02NumerosNaturales[2].data[5] = new Taller02NumerosNaturalesData();

        // -10 ∧ 3 = 1000, -1000, -30, 30
		juego.taller02NumerosNaturales[2].data[0].operacion = "-10 ^ 3 ";
        juego.taller02NumerosNaturales[2].data[0].resultados.Add("-1000");
        juego.taller02NumerosNaturales[2].data[0].addOpciones(new int[] { 1000, -1000, -30, 30 });
        

        // ∛-8 = 2, 4, -4, -2
        juego.taller02NumerosNaturales[2].data[1].operacion = "∛-8 ";
        juego.taller02NumerosNaturales[2].data[1].resultados.Add("-2");
        juego.taller02NumerosNaturales[2].data[1].addOpciones(new int[] { 2, 4, -4, -2 });

        // √16 = -4, 4, 2, -2
        juego.taller02NumerosNaturales[2].data[2].operacion = "√16 ";
        juego.taller02NumerosNaturales[2].data[2].resultados.Add("(+-)4");
        juego.taller02NumerosNaturales[2].data[2].addOpciones(new int[] { -4, 4, 2, -2  });
        juego.taller02NumerosNaturales[2].data[2].opciones.Add("(+-)4");
        // ∜-16 = 2, -2, 4, -4, ∄						
        juego.taller02NumerosNaturales[2].data[3].operacion = "∜-16"; 
        juego.taller02NumerosNaturales[2].data[3].resultados.Add("∄");
        juego.taller02NumerosNaturales[2].data[3].addOpciones(new int[] { 2, -2, 4, -4}); 
        juego.taller02NumerosNaturales[2].data[3].opciones.Add("∄");

		// 4 ^ 3 = 12, 24, -12, 64
        juego.taller02NumerosNaturales[2].data[4].operacion = "4 ^ 3";
        juego.taller02NumerosNaturales[2].data[4].resultados.Add("64");
        juego.taller02NumerosNaturales[2].data[4].addOpciones(new int[] { 12, 24, -12, 64 });

		// 7 ^ 2 = 128, 14, 49, 7
		juego.taller02NumerosNaturales[2].data[5].operacion = "7 ^ 2";
        juego.taller02NumerosNaturales[2].data[5].resultados.Add("49");
        juego.taller02NumerosNaturales[2].data[5].addOpciones(new int[] { 128, 14, 49, 7 });

        puntaje = 0;
        foreach (Taller02NumerosNaturalesData a in juego.taller02NumerosNaturales[2].data)
        {
            puntaje += a.puntoOK;
        }
        juego.taller02NumerosNaturales[2].puntajeMaximo = puntaje;




        //---------------------------------------------------------------------------------------
        /*Ecuaciones*/
        juego.taller03Ecuaciones = new Taller03Ecuaciones[3];
		juego.taller03Ecuaciones[0] = new Taller03Ecuaciones();
		juego.taller03Ecuaciones [0].puntajeMaximo = 1250;
		juego.taller03Ecuaciones[0].data =  new Taller03EcuacionesData[5]; 

		//Nivel 1 - Primera Ecuación
		juego.taller03Ecuaciones[0].data [0] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[0].data [0].resultado = -12f;
		juego.taller03Ecuaciones[0].data [0].planteoIzquierda = "x - 15";
		juego.taller03Ecuaciones[0].data [0].planteoDerecha = "-27";

		juego.taller03Ecuaciones[0].data [0].validosIzquierda = new string[2][];
		juego.taller03Ecuaciones[0].data [0].validosDerecha = new string[2][];
		juego.taller03Ecuaciones[0].data [0].noValidos = new string[2][];


		juego.taller03Ecuaciones[0].data[0].validosIzquierda[0] = new string[]{"x"};
		juego.taller03Ecuaciones[0].data[0].validosDerecha[0]   = new string[]{"-27", "+15"};
		juego.taller03Ecuaciones[0].data [0].noValidos[0] 	   = new string[]{"27", "-15" };

		juego.taller03Ecuaciones[0].data[0].validosIzquierda[1] = new string[]{"x"};
		juego.taller03Ecuaciones[0].data[0].validosDerecha[1]   = new string[]{"-12"};
		juego.taller03Ecuaciones[0].data[0].noValidos[1] 	   = new string[]{"12", "42"};
        //Nivel 1 - Segunda Ecuación
		juego.taller03Ecuaciones[0].data [1] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[0].data [1].resultado = -12f;
		juego.taller03Ecuaciones[0].data [1].planteoIzquierda = "-11x +12";
		juego.taller03Ecuaciones[0].data [1].planteoDerecha = "144";

		juego.taller03Ecuaciones[0].data [1].validosIzquierda = new string[4][];
		juego.taller03Ecuaciones[0].data [1].validosDerecha = new string[4][];
		juego.taller03Ecuaciones[0].data [1].noValidos = new string[4][];


		juego.taller03Ecuaciones[0].data[1].validosIzquierda[0] = new string[]{"-", "11x"};
		juego.taller03Ecuaciones[0].data[1].validosDerecha[0]   = new string[]{"144", "-12"};
		juego.taller03Ecuaciones[0].data [1].noValidos[0] 	   = new string[]{"+", "+12" };

		juego.taller03Ecuaciones[0].data[1].validosIzquierda[1] = new string[]{"-", "11x"};
		juego.taller03Ecuaciones[0].data[1].validosDerecha[1]   = new string[]{"132"};
		juego.taller03Ecuaciones[0].data[1].noValidos[1] 	   = new string[]{"156", "12"};

		juego.taller03Ecuaciones[0].data[1].validosIzquierda[2] = new string[]{"x"};
		juego.taller03Ecuaciones[0].data[1].validosDerecha[2]   = new string[]{"132", ":", "(-11)"};
		juego.taller03Ecuaciones[0].data[1].noValidos[2] 	   = new string[]{"11", "."};

		juego.taller03Ecuaciones[0].data[1].validosIzquierda[3] = new string[]{"x"};
		juego.taller03Ecuaciones[0].data[1].validosDerecha[3]   = new string[]{"-12"};
		juego.taller03Ecuaciones[0].data[1].noValidos[3] 	   = new string[]{"12"};

		//Nivel 1 - Tercera Ecuación
		juego.taller03Ecuaciones[0].data [2] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[0].data [2].resultado = 12f;
		juego.taller03Ecuaciones[0].data [2].planteoIzquierda = "-8x -15";
		juego.taller03Ecuaciones[0].data [2].planteoDerecha = "-111";

		juego.taller03Ecuaciones[0].data [2].validosIzquierda = new string[4][];
		juego.taller03Ecuaciones[0].data [2].validosDerecha = new string[4][];
		juego.taller03Ecuaciones[0].data [2].noValidos = new string[4][];


		juego.taller03Ecuaciones[0].data[2].validosIzquierda[0] = new string[]{"-", "8x"};
		juego.taller03Ecuaciones[0].data[2].validosDerecha[0]   = new string[]{"-111", "+15"};
		juego.taller03Ecuaciones[0].data[2].noValidos[0] 	   = new string[]{":","8", "-15" };

		juego.taller03Ecuaciones[0].data[2].validosIzquierda[1] = new string[]{"-","8x"};
		juego.taller03Ecuaciones[0].data[2].validosDerecha[1]   = new string[]{"-96"};
		juego.taller03Ecuaciones[0].data[2].noValidos[1] 	   = new string[]{"126", "12"};

		juego.taller03Ecuaciones[0].data[2].validosIzquierda[2] = new string[]{"x"};
		juego.taller03Ecuaciones[0].data[2].validosDerecha[2]   = new string[]{"(-96)", ":", "(-8)"};
		juego.taller03Ecuaciones[0].data[2].noValidos[2] 	   = new string[]{"8", "."};

		juego.taller03Ecuaciones[0].data[2].validosIzquierda[3] = new string[]{"x"};
		juego.taller03Ecuaciones[0].data[2].validosDerecha[3]   = new string[]{"12"};
		juego.taller03Ecuaciones[0].data[2].noValidos[3] 	   = new string[]{"-12"};


		//Nivel 1 - Cuarta Ecuación
		juego.taller03Ecuaciones[0].data [3] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[0].data [3].resultado = 9f;
		juego.taller03Ecuaciones[0].data [3].planteoIzquierda = "-10x +9";
		juego.taller03Ecuaciones[0].data [3].planteoDerecha = "-81";

		juego.taller03Ecuaciones[0].data [3].validosIzquierda = new string[4][];
		juego.taller03Ecuaciones[0].data [3].validosDerecha = new string[4][];
		juego.taller03Ecuaciones[0].data [3].noValidos = new string[4][];


		juego.taller03Ecuaciones[0].data[3].validosIzquierda[0] = new string[]{"-","10x"};
		juego.taller03Ecuaciones[0].data[3].validosDerecha[0]   = new string[]{"-81", "-9"};
		juego.taller03Ecuaciones[0].data[3].noValidos[0] 	   = new string[]{":","+9", "10" };

		juego.taller03Ecuaciones[0].data[3].validosIzquierda[1] = new string[]{"-","10x"};
		juego.taller03Ecuaciones[0].data[3].validosDerecha[1]   = new string[]{"-90"};
		juego.taller03Ecuaciones[0].data[3].noValidos[1] 	   = new string[]{"-72"};

		juego.taller03Ecuaciones[0].data[3].validosIzquierda[2] = new string[]{"x"};
		juego.taller03Ecuaciones[0].data[3].validosDerecha[2]   = new string[]{"(-90)", ":", "(-10)"};
		juego.taller03Ecuaciones[0].data[3].noValidos[2] 	   = new string[]{"81", "."};

		juego.taller03Ecuaciones[0].data[3].validosIzquierda[3] = new string[]{"x"};
		juego.taller03Ecuaciones[0].data[3].validosDerecha[3]   = new string[]{"9"};
		juego.taller03Ecuaciones[0].data[3].noValidos[3] 	   = new string[]{"-9"};

		//Nivel 1 - Quinta Ecuación
		juego.taller03Ecuaciones[0].data [4] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[0].data [4].resultado = -15f;
		juego.taller03Ecuaciones[0].data [4].planteoIzquierda = "7x +5";
		juego.taller03Ecuaciones[0].data [4].planteoDerecha = "-100";

		juego.taller03Ecuaciones[0].data [4].validosIzquierda = new string[4][];
		juego.taller03Ecuaciones[0].data [4].validosDerecha = new string[4][];
		juego.taller03Ecuaciones[0].data [4].noValidos = new string[4][];


		juego.taller03Ecuaciones[0].data[4].validosIzquierda[0] = new string[]{"7x"};
		juego.taller03Ecuaciones[0].data[4].validosDerecha[0]   = new string[]{"-100", "-5"};
		juego.taller03Ecuaciones[0].data[4].noValidos[0] 	   = new string[]{":","7", "+5" };

		juego.taller03Ecuaciones[0].data[4].validosIzquierda[1] = new string[]{"7x"};
		juego.taller03Ecuaciones[0].data[4].validosDerecha[1]   = new string[]{"-105"};
		juego.taller03Ecuaciones[0].data[4].noValidos[1] 	   = new string[]{"-95"};

		juego.taller03Ecuaciones[0].data[4].validosIzquierda[2] = new string[]{"x"};
		juego.taller03Ecuaciones[0].data[4].validosDerecha[2]   = new string[]{"(-105)", ":", "7"};
		juego.taller03Ecuaciones[0].data[4].noValidos[2] 	   = new string[]{"(-7)", "."};

		juego.taller03Ecuaciones[0].data[4].validosIzquierda[3] = new string[]{"x"};
		juego.taller03Ecuaciones[0].data[4].validosDerecha[3]   = new string[]{"-15"};
		juego.taller03Ecuaciones[0].data[4].noValidos[3] 	   = new string[]{"15","10"};


		//Nivel 2 - Primera Ecuación
		/*
		2x-3=6+x    
		2x-x=6+3        
		x=9
		*/
		juego.taller03Ecuaciones[1] = new Taller03Ecuaciones();
		juego.taller03Ecuaciones [1].puntajeMaximo = 2500;
		juego.taller03Ecuaciones[1].data =  new Taller03EcuacionesData[10]; 

		juego.taller03Ecuaciones[1].data [0] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[1].data [0].resultado = 9f;
		juego.taller03Ecuaciones[1].data [0].planteoIzquierda = "2x - 3";
		juego.taller03Ecuaciones[1].data [0].planteoDerecha = "6 + x";

		juego.taller03Ecuaciones[1].data [0].validosIzquierda = new string[2][];
		juego.taller03Ecuaciones[1].data [0].validosDerecha = new string[2][];
		juego.taller03Ecuaciones[1].data [0].noValidos = new string[2][];


		juego.taller03Ecuaciones[1].data[0].validosIzquierda[0] = new string[]{"2x" , "- x"};
		juego.taller03Ecuaciones[1].data[0].validosDerecha[0]   = new string[]{"6", "+", "3"};
		juego.taller03Ecuaciones[1].data [0].noValidos[0] 	   = new string[]{"-6", "-" };

		juego.taller03Ecuaciones[1].data[0].validosIzquierda[1] = new string[]{"x"};
		juego.taller03Ecuaciones[1].data[0].validosDerecha[1]   = new string[]{"9"};
		juego.taller03Ecuaciones[1].data[0].noValidos[1] 	   = new string[]{"3", "-9"};

		//Nivel 2 - Segunda Ecuación
		/*
		2(2x-3)=6+x       
		4x-6=6+x           
		4x-x=6+6        
		3x=12       
		x=12/3      
		x=4
		*/
		juego.taller03Ecuaciones[1].data [1] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[1].data [1].resultado = 4f;
		juego.taller03Ecuaciones[1].data [1].planteoIzquierda = "2(2x - 3)";
		juego.taller03Ecuaciones[1].data [1].planteoDerecha = "6 + x";

		juego.taller03Ecuaciones[1].data [1].validosIzquierda = new string[5][];
		juego.taller03Ecuaciones[1].data [1].validosDerecha = new string[5][];
		juego.taller03Ecuaciones[1].data [1].noValidos = new string[5][];


		juego.taller03Ecuaciones[1].data[1].validosIzquierda[0] = new string[]{"4x", "-6"};
		juego.taller03Ecuaciones[1].data[1].validosDerecha[0]   = new string[]{"6 ", "+ x"};
		juego.taller03Ecuaciones[1].data [1].noValidos[0] 	   = new string[]{"+ 6" };

		juego.taller03Ecuaciones[1].data[1].validosIzquierda[1] = new string[]{"4x", "-", "x"};
		juego.taller03Ecuaciones[1].data[1].validosDerecha[1]   = new string[]{"6","+", "6"};
		juego.taller03Ecuaciones[1].data[1].noValidos[1] 	   = new string[]{"(-6)"};

		juego.taller03Ecuaciones[1].data[1].validosIzquierda[2] = new string[]{"3x"};
		juego.taller03Ecuaciones[1].data[1].validosDerecha[2]   = new string[]{"12"};
		juego.taller03Ecuaciones[1].data[1].noValidos[2] 	   = new string[]{"-12", "3"};

		juego.taller03Ecuaciones[1].data[1].validosIzquierda[3] = new string[]{"x"};
		juego.taller03Ecuaciones[1].data[1].validosDerecha[3]   = new string[]{"12", ":", "3"};
		juego.taller03Ecuaciones[1].data[1].noValidos[3] 	   = new string[]{".", "(-3)"};

		juego.taller03Ecuaciones[1].data[1].validosIzquierda[4] = new string[]{"x"};
		juego.taller03Ecuaciones[1].data[1].validosDerecha[4]   = new string[]{"4"};
		juego.taller03Ecuaciones[1].data[1].noValidos[4] 	   = new string[]{"-4", "3"};

		//Nivel 2 - Tercera Ecuación
		/*
		5/(x-7 ) = 4/(x-2)        
		5(x-2)=4(x-7)       
		5x-10=4x-28         
		5x-4x=-28+10      
		x=-18
		*/
		juego.taller03Ecuaciones[1].data [2] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[1].data [2].resultado = -18f;
		juego.taller03Ecuaciones[1].data [2].planteoIzquierda = "5 : (x - 7)";
		juego.taller03Ecuaciones[1].data [2].planteoDerecha = "4 : (x - 2)";

		juego.taller03Ecuaciones[1].data [2].validosIzquierda = new string[4][];
		juego.taller03Ecuaciones[1].data [2].validosDerecha = new string[4][];
		juego.taller03Ecuaciones[1].data [2].noValidos = new string[4][];


		juego.taller03Ecuaciones[1].data[2].validosIzquierda[0] = new string[]{"5", ".", "(x-2)"};
		juego.taller03Ecuaciones[1].data[2].validosDerecha[0]   = new string[]{"4", ".", "(x-7)"};
		juego.taller03Ecuaciones[1].data [2].noValidos[0] 	   = new string[]{"-", "+" };

		juego.taller03Ecuaciones[1].data[2].validosIzquierda[1] = new string[]{"5x", "-", "10"};
		juego.taller03Ecuaciones[1].data[2].validosDerecha[1]   = new string[]{"4x", "-", "28"};
		juego.taller03Ecuaciones[1].data[2].noValidos[1] 	   = new string[]{"+"};

		juego.taller03Ecuaciones[1].data[2].validosIzquierda[2] = new string[]{"5x", "-", "4x"};
		juego.taller03Ecuaciones[1].data[2].validosDerecha[2]   = new string[]{"-28","+", "10"};
		juego.taller03Ecuaciones[1].data [2].noValidos[2] 	   = new string[]{"28", "-10" };
		
		juego.taller03Ecuaciones[1].data[2].validosIzquierda[3] = new string[]{"x"};
		juego.taller03Ecuaciones[1].data[2].validosDerecha[3]   = new string[]{"-18"};
		juego.taller03Ecuaciones[1].data[2].noValidos[3] 	   = new string[]{"18", "-38"};
		
		//Nivel 2 - Cuarta Ecuación
		/*
		4/(x-3) = 5/(x-2)   
		4(x-2)=5(x-3)     
		4x-8=5x-15  
		4x-5x=-15+8      
		-x=-7    
		x=7
		*/
		juego.taller03Ecuaciones[1].data [3] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[1].data [3].resultado = 7f;
		juego.taller03Ecuaciones[1].data [3].planteoIzquierda = "4 : (x-3)";
		juego.taller03Ecuaciones[1].data [3].planteoDerecha = "5 :(x-2)";

		juego.taller03Ecuaciones[1].data [3].validosIzquierda = new string[5][];
		juego.taller03Ecuaciones[1].data [3].validosDerecha = new string[5][];
		juego.taller03Ecuaciones[1].data [3].noValidos = new string[5][];


		juego.taller03Ecuaciones[1].data[3].validosIzquierda[0] = new string[]{"4", ".", "(x-2)"};
		juego.taller03Ecuaciones[1].data[3].validosDerecha[0]   = new string[]{"5", ".", "(x-3)"};
		juego.taller03Ecuaciones[1].data[3].noValidos[0] 	   = new string[]{":", "(x+3)" };

		juego.taller03Ecuaciones[1].data[3].validosIzquierda[1] = new string[]{"4x","-8"};
		juego.taller03Ecuaciones[1].data[3].validosDerecha[1]   = new string[]{"5x","-" ,"15"};
		juego.taller03Ecuaciones[1].data[3].noValidos[1] 	   = new string[]{"+8", ":"};

		juego.taller03Ecuaciones[1].data[3].validosIzquierda[2] = new string[]{"4x", "-", "5x"};
		juego.taller03Ecuaciones[1].data[3].validosDerecha[2]   = new string[]{"-15", "+", "8"};
		juego.taller03Ecuaciones[1].data[3].noValidos[2] 	   = new string[]{"15", "-4x"};

		juego.taller03Ecuaciones[1].data[3].validosIzquierda[3] = new string[]{"-x"};
		juego.taller03Ecuaciones[1].data[3].validosDerecha[3]   = new string[]{"-7"};
		juego.taller03Ecuaciones[1].data[3].noValidos[3] 	   = new string[]{"-23"};

		juego.taller03Ecuaciones[1].data[3].validosIzquierda[4] = new string[]{"x"};
		juego.taller03Ecuaciones[1].data[3].validosDerecha[4]   = new string[]{"7"};
		juego.taller03Ecuaciones[1].data[3].noValidos[4] 	   = new string[]{"-7"};

		//Nivel 2 - Quinta Ecuación
		/*
		 3x+1=3-(2-2x)    
		 3x+1 =3-2+2x     
		 3x-2x =3-2-1  
		 x=0
		*/
		juego.taller03Ecuaciones[1].data [4] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[1].data [4].resultado = 0f;
		juego.taller03Ecuaciones[1].data [4].planteoIzquierda = "3x+1";
		juego.taller03Ecuaciones[1].data [4].planteoDerecha = "3-(2-2x)";

		juego.taller03Ecuaciones[1].data [4].validosIzquierda = new string[3][];
		juego.taller03Ecuaciones[1].data [4].validosDerecha = new string[3][];
		juego.taller03Ecuaciones[1].data [4].noValidos = new string[3][];


		juego.taller03Ecuaciones[1].data[4].validosIzquierda[0] = new string[]{"3x", "+", "1"};
		juego.taller03Ecuaciones[1].data[4].validosDerecha[0]   = new string[]{"3", "-", "2", "+", "2x" };
		juego.taller03Ecuaciones[1].data [4].noValidos[0] 	   = new string[]{"-3" };

		juego.taller03Ecuaciones[1].data[4].validosIzquierda[1] = new string[]{"3x", "-", "2x"};
		juego.taller03Ecuaciones[1].data[4].validosDerecha[1]   = new string[]{"3", "-2", "-1"};
		juego.taller03Ecuaciones[1].data[4].noValidos[1] 	   = new string[]{"+2", "+1"};

		juego.taller03Ecuaciones[1].data[4].validosIzquierda[2] = new string[]{"x"};
		juego.taller03Ecuaciones[1].data[4].validosDerecha[2]   = new string[]{"0"};
		juego.taller03Ecuaciones[1].data[4].noValidos[2] 	   = new string[]{"3"};


		//Nivel 2 - Sexta Ecuación
		/*
		3+x-2=3+1         
		x=3+1-3+2     
		x=3
		*/
		juego.taller03Ecuaciones[1].data [5] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[1].data [5].resultado = 3f;
		juego.taller03Ecuaciones[1].data [5].planteoIzquierda = "3+x-2";
		juego.taller03Ecuaciones[1].data [5].planteoDerecha = "3+1";

		juego.taller03Ecuaciones[1].data [5].validosIzquierda = new string[2][];
		juego.taller03Ecuaciones[1].data [5].validosDerecha = new string[2][];
		juego.taller03Ecuaciones[1].data [5].noValidos = new string[2][];


		juego.taller03Ecuaciones[1].data[5].validosIzquierda[0] = new string[]{"x"};
		juego.taller03Ecuaciones[1].data[5].validosDerecha[0]   = new string[]{"3", "+1", "-3", "+2"};
		juego.taller03Ecuaciones[1].data [5].noValidos[0] 	   = new string[]{"-2" };

		juego.taller03Ecuaciones[1].data[5].validosIzquierda[1] = new string[]{"x"};
		juego.taller03Ecuaciones[1].data[5].validosDerecha[1]   = new string[]{"3"};
		juego.taller03Ecuaciones[1].data[5].noValidos[1] 	   = new string[]{"-3", "1"};

		//Nivel 2 - Septima Ecuación
		/*
		6-x-2=-2+5   
		4-x=3     
		-x=3-4       
		-x=-1    
		 x=1
		*/
		juego.taller03Ecuaciones[1].data [6] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[1].data [6].resultado = 1f;
		juego.taller03Ecuaciones[1].data [6].planteoIzquierda = "6-x-2";
		juego.taller03Ecuaciones[1].data [6].planteoDerecha = "-2+5";

		juego.taller03Ecuaciones[1].data [6].validosIzquierda = new string[4][];
		juego.taller03Ecuaciones[1].data [6].validosDerecha = new string[4][];
		juego.taller03Ecuaciones[1].data [6].noValidos = new string[4][];


		juego.taller03Ecuaciones[1].data[6].validosIzquierda[0] = new string[]{"4", "-x"};
		juego.taller03Ecuaciones[1].data[6].validosDerecha[0]   = new string[]{"3"};
		juego.taller03Ecuaciones[1].data [6].noValidos[0] 	   = new string[]{"-3" };

		juego.taller03Ecuaciones[1].data[6].validosIzquierda[1] = new string[]{"-x"};
		juego.taller03Ecuaciones[1].data[6].validosDerecha[1]   = new string[]{"3", "-", "4"};
		juego.taller03Ecuaciones[1].data[6].noValidos[1] 	   = new string[]{"+"};

		juego.taller03Ecuaciones[1].data[6].validosIzquierda[2] = new string[]{"-x"};
		juego.taller03Ecuaciones[1].data[6].validosDerecha[2]   = new string[]{"-1"};
		juego.taller03Ecuaciones[1].data[6].noValidos[2] 	   = new string[]{"1"};

		juego.taller03Ecuaciones[1].data[6].validosIzquierda[3] = new string[]{"x"};
		juego.taller03Ecuaciones[1].data[6].validosDerecha[3]   = new string[]{"1"};
		juego.taller03Ecuaciones[1].data[6].noValidos[3] 	   = new string[]{"-1"};


		//Nivel 2 - Octava Ecuación
		/*
		3(2-x)=3-2x   
		6-3x=3-2x        
		-3x+2x=3-6       
		-x=-3     
		x=3
		*/
		juego.taller03Ecuaciones[1].data [7] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[1].data [7].resultado = 3f;
		juego.taller03Ecuaciones[1].data [7].planteoIzquierda = "3(2-x)";
		juego.taller03Ecuaciones[1].data [7].planteoDerecha = "3-2x";

		juego.taller03Ecuaciones[1].data [7].validosIzquierda = new string[4][];
		juego.taller03Ecuaciones[1].data [7].validosDerecha = new string[4][];
		juego.taller03Ecuaciones[1].data [7].noValidos = new string[4][];


		juego.taller03Ecuaciones[1].data[7].validosIzquierda[0] = new string[]{"6", "-", "3x"};
		juego.taller03Ecuaciones[1].data[7].validosDerecha[0]   = new string[]{"3", "-", "2x"};
		juego.taller03Ecuaciones[1].data [7].noValidos[0] 	   = new string[]{"-3", "+" };

		juego.taller03Ecuaciones[1].data[7].validosIzquierda[1] = new string[]{"-","3x", "+", "2x"};
		juego.taller03Ecuaciones[1].data[7].validosDerecha[1]   = new string[]{"3", "-", "6"};
		juego.taller03Ecuaciones[1].data[7].noValidos[1] 	   = new string[]{"-2x"};

		juego.taller03Ecuaciones[1].data[7].validosIzquierda[2] = new string[]{"-x"};
		juego.taller03Ecuaciones[1].data[7].validosDerecha[2]   = new string[]{"-3"};
		juego.taller03Ecuaciones[1].data [7].noValidos[2] 	   = new string[]{"3"};

		juego.taller03Ecuaciones[1].data[7].validosIzquierda[3] = new string[]{"x"};
		juego.taller03Ecuaciones[1].data[7].validosDerecha[3]   = new string[]{"3"};
		juego.taller03Ecuaciones[1].data[7].noValidos[3] 	   = new string[]{"-3"};

		//Nivel 2 - Novena Ecuación
		/*
		-3(3-x)=-6x      
		-9+3x= -6x         
		3x+6x  = 9     
		9x= 9      
		x=1
		*/
		juego.taller03Ecuaciones[1].data [8] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[1].data [8].resultado = 1f;
		juego.taller03Ecuaciones[1].data [8].planteoIzquierda = "-3 . (3-x)";
		juego.taller03Ecuaciones[1].data [8].planteoDerecha = "-6x";

		juego.taller03Ecuaciones[1].data [8].validosIzquierda = new string[4][];
		juego.taller03Ecuaciones[1].data [8].validosDerecha = new string[4][];
		juego.taller03Ecuaciones[1].data [8].noValidos = new string[4][];


		juego.taller03Ecuaciones[1].data[8].validosIzquierda[0] = new string[]{"-9", "+", "3x"};
		juego.taller03Ecuaciones[1].data[8].validosDerecha[0]   = new string[]{"-", "6x"};
		juego.taller03Ecuaciones[1].data [8].noValidos[0] 	   = new string[]{"9", "6" };

		juego.taller03Ecuaciones[1].data[8].validosIzquierda[1] = new string[]{"3x", "+", "6x"};
		juego.taller03Ecuaciones[1].data[8].validosDerecha[1]   = new string[]{"9"};
		juego.taller03Ecuaciones[1].data[8].noValidos[1] 	   = new string[]{"(-9)", "-"};

		juego.taller03Ecuaciones[1].data[8].validosIzquierda[2] = new string[]{"9x"};
		juego.taller03Ecuaciones[1].data[8].validosDerecha[2]   = new string[]{"9"};
		juego.taller03Ecuaciones[1].data [8].noValidos[2] 	   = new string[]{"-9", "3x" };

		juego.taller03Ecuaciones[1].data[8].validosIzquierda[3] = new string[]{"x"};
		juego.taller03Ecuaciones[1].data[8].validosDerecha[3]   = new string[]{"1"};
		juego.taller03Ecuaciones[1].data[8].noValidos[3] 	   = new string[]{"81", "18"};

		
		//Nivel 2 - Décima Ecuación
		/*
		10+2x-5.3=7-2.5       
		10+2x-15= 7-10      
		-5+2x=-3     
		2x=-3+5     
		2x=2      
		x=1
		*/
		juego.taller03Ecuaciones[1].data [9] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[1].data [9].resultado = 1f;
		juego.taller03Ecuaciones[1].data [9].planteoIzquierda = "10+2x-5.3";
		juego.taller03Ecuaciones[1].data [9].planteoDerecha = "7-2.5";

		juego.taller03Ecuaciones[1].data [9].validosIzquierda = new string[5][];
		juego.taller03Ecuaciones[1].data [9].validosDerecha = new string[5][];
		juego.taller03Ecuaciones[1].data [9].noValidos = new string[5][];


		juego.taller03Ecuaciones[1].data[9].validosIzquierda[0] = new string[]{"10", "+","2x", "-15"};
		juego.taller03Ecuaciones[1].data[9].validosDerecha[0]   = new string[]{"7", "-", "10"};
		juego.taller03Ecuaciones[1].data[9].noValidos[0] 	   = new string[]{"-", "+15" };

		juego.taller03Ecuaciones[1].data[9].validosIzquierda[1] = new string[]{"-5", "2x"};
		juego.taller03Ecuaciones[1].data[9].validosDerecha[1]   = new string[]{"-3"};
		juego.taller03Ecuaciones[1].data[9].noValidos[1] 	   = new string[]{"3"};

		juego.taller03Ecuaciones[1].data[9].validosIzquierda[2] = new string[]{"2x"};
		juego.taller03Ecuaciones[1].data[9].validosDerecha[2]   = new string[]{"-3", "+", "5"};
		juego.taller03Ecuaciones[1].data[9].noValidos[2] 	   = new string[]{"-"};

		juego.taller03Ecuaciones[1].data[9].validosIzquierda[3] = new string[]{"2x"};
		juego.taller03Ecuaciones[1].data[9].validosDerecha[3]   = new string[]{"2"};
		juego.taller03Ecuaciones[1].data[9].noValidos[3] 	   = new string[]{"-2"};

		juego.taller03Ecuaciones[1].data[9].validosIzquierda[4] = new string[]{"x"};
		juego.taller03Ecuaciones[1].data[9].validosDerecha[4]   = new string[]{"1"};
		juego.taller03Ecuaciones[1].data[9].noValidos[4] 	   = new string[]{"4"};
		
		
		//Nivel 3 - Primera Ecuación
		/*
		-5 + x = (-5) . (-4)    
		-5+ x = 20       
		x = 20 +5    
		x = 25 
		*/
		juego.taller03Ecuaciones[2] = new Taller03Ecuaciones();
		juego.taller03Ecuaciones[2].puntajeMaximo = 2500;
		juego.taller03Ecuaciones[2].data =  new Taller03EcuacionesData[10]; 
		
		juego.taller03Ecuaciones[2].data [0] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[2].data [0].resultado = 25f;
		juego.taller03Ecuaciones[2].data [0].planteoIzquierda = "-5 + x";
		juego.taller03Ecuaciones[2].data [0].planteoDerecha = "(-5) . (-4)";

		juego.taller03Ecuaciones[2].data [0].validosIzquierda = new string[3][];
		juego.taller03Ecuaciones[2].data [0].validosDerecha = new string[3][];
		juego.taller03Ecuaciones[2].data [0].noValidos = new string[3][];


		juego.taller03Ecuaciones[2].data[0].validosIzquierda[0] = new string[]{"-5" , "+x"};
		juego.taller03Ecuaciones[2].data[0].validosDerecha[0]   = new string[]{"20"};
		juego.taller03Ecuaciones[2].data[0].noValidos[0] 	   = new string[]{"-20" };

		juego.taller03Ecuaciones[2].data[0].validosIzquierda[1] = new string[]{"x"};
		juego.taller03Ecuaciones[2].data[0].validosDerecha[1]   = new string[]{"20","+5"};
		juego.taller03Ecuaciones[2].data[0].noValidos[1] 	   = new string[]{".", "(-5)"};

		juego.taller03Ecuaciones[2].data[0].validosIzquierda[2] = new string[]{"x"};
		juego.taller03Ecuaciones[2].data[0].validosDerecha[2]   = new string[]{"25"};
		juego.taller03Ecuaciones[2].data[0].noValidos[2] 	   = new string[]{"15", "-20"};

		//Nivel 3 - Segunda Ecuación
		/*
		-9 -15 + x = 8 - 5 - 12          
		-24+x = -9
		x=-9+24
		x=15
		*/
		
		juego.taller03Ecuaciones[2].data [1] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[2].data [1].resultado = 15f;
		juego.taller03Ecuaciones[2].data [1].planteoIzquierda = "-9 -15 + x";
		juego.taller03Ecuaciones[2].data [1].planteoDerecha = "8 - 5 - 12";

		juego.taller03Ecuaciones[2].data [1].validosIzquierda = new string[3][];
		juego.taller03Ecuaciones[2].data [1].validosDerecha = new string[3][];
		juego.taller03Ecuaciones[2].data [1].noValidos = new string[3][];


		juego.taller03Ecuaciones[2].data[1].validosIzquierda[0] = new string[]{"-24" , "+x"};
		juego.taller03Ecuaciones[2].data[1].validosDerecha[0]   = new string[]{"-9"};
		juego.taller03Ecuaciones[2].data[1].noValidos[0] 	   = new string[]{"9", "-6" };

		juego.taller03Ecuaciones[2].data[1].validosIzquierda[1] = new string[]{"x"};
		juego.taller03Ecuaciones[2].data[1].validosDerecha[1]   = new string[]{"-9","+24"};
		juego.taller03Ecuaciones[2].data[1].noValidos[1] 	   = new string[]{":", "-24"};

		juego.taller03Ecuaciones[2].data[1].validosIzquierda[2] = new string[]{"x"};
		juego.taller03Ecuaciones[2].data[1].validosDerecha[2]   = new string[]{"15"};
		juego.taller03Ecuaciones[2].data[1].noValidos[2] 	   = new string[]{"-15", "33"};


		//Nivel 3 - Tercera Ecuación
		/*
		x - 6 - 3 = - 37                      
		x - 9 = -37 
		x= -37+9
		x= -28
		*/
		
		juego.taller03Ecuaciones[2].data [2] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[2].data [2].resultado = -28f;
		juego.taller03Ecuaciones[2].data [2].planteoIzquierda = "x - 6 - 3";
		juego.taller03Ecuaciones[2].data [2].planteoDerecha = "-37";

		juego.taller03Ecuaciones[2].data [2].validosIzquierda = new string[3][];
		juego.taller03Ecuaciones[2].data [2].validosDerecha = new string[3][];
		juego.taller03Ecuaciones[2].data [2].noValidos = new string[3][];


		juego.taller03Ecuaciones[2].data[2].validosIzquierda[0] = new string[]{"x", "- 9"};
		juego.taller03Ecuaciones[2].data[2].validosDerecha[0]   = new string[]{"-37"};
		juego.taller03Ecuaciones[2].data[2].noValidos[0] 	    = new string[]{"9", "-3" };

		juego.taller03Ecuaciones[2].data[2].validosIzquierda[1] = new string[]{"x"};
		juego.taller03Ecuaciones[2].data[2].validosDerecha[1]   = new string[]{"-37", "+", "9"};
		juego.taller03Ecuaciones[2].data[2].noValidos[1] 	    = new string[]{"-", "37"};

		juego.taller03Ecuaciones[2].data[2].validosIzquierda[2] = new string[]{"x"};
		juego.taller03Ecuaciones[2].data[2].validosDerecha[2]   = new string[]{"-28"};
		juego.taller03Ecuaciones[2].data[2].noValidos[2] 	    = new string[]{"-46", "28"};

		//Nivel 3 - Cuarta Ecuación
		/*
		- 32 + 47 - x = 18 - (-3)        
		15-x=21
		-x=21-15
		-x=6
		x=-6		
		*/
		juego.taller03Ecuaciones[2].data [3] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[2].data [3].resultado = -6f;
		juego.taller03Ecuaciones[2].data [3].planteoIzquierda = "-32 + 47 - x";
		juego.taller03Ecuaciones[2].data [3].planteoDerecha = "18 - (-3)";

		juego.taller03Ecuaciones[2].data [3].validosIzquierda = new string[4][];
		juego.taller03Ecuaciones[2].data [3].validosDerecha = new string[4][];
		juego.taller03Ecuaciones[2].data [3].noValidos = new string[4][];


		juego.taller03Ecuaciones[2].data[3].validosIzquierda[0] = new string[]{"15", "-", "x"};
		juego.taller03Ecuaciones[2].data[3].validosDerecha[0]   = new string[]{"21"};
		juego.taller03Ecuaciones[2].data[3].noValidos[0] 	   = new string[]{"+", "-21", "-15" };

		juego.taller03Ecuaciones[2].data[3].validosIzquierda[1] = new string[]{"-x"};
		juego.taller03Ecuaciones[2].data[3].validosDerecha[1]   = new string[]{"21", "-", "15"};
		juego.taller03Ecuaciones[2].data[3].noValidos[1] 	   = new string[]{"+", "-21"};

		juego.taller03Ecuaciones[2].data[3].validosIzquierda[2] = new string[]{"-x"};
		juego.taller03Ecuaciones[2].data[3].validosDerecha[2]   = new string[]{"6"};
		juego.taller03Ecuaciones[2].data[3].noValidos[2] 	   = new string[]{"-6", "-36"};

		juego.taller03Ecuaciones[2].data[3].validosIzquierda[3] = new string[]{"x"};
		juego.taller03Ecuaciones[2].data[3].validosDerecha[3]   = new string[]{"-6"};
		juego.taller03Ecuaciones[2].data[3].noValidos[3] 	   = new string[]{"6"};

		//Nivel 3 - Quinta Ecuación
		/*
		5 - (3 + 8) + x = 6 - (-3 +2)   
		5 -11+ x = 6 - (-1)
		-6 +x=6+1
		x=7+6   
		x=13	
		*/
		juego.taller03Ecuaciones[2].data[4] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[2].data[4].resultado = 13f;
		juego.taller03Ecuaciones[2].data[4].planteoIzquierda = "5 - (3 + 8) + x";
		juego.taller03Ecuaciones[2].data[4].planteoDerecha = "6 - (-3 + 2)";

		juego.taller03Ecuaciones[2].data[4].validosIzquierda = new string[4][];
		juego.taller03Ecuaciones[2].data[4].validosDerecha = new string[4][];
		juego.taller03Ecuaciones[2].data[4].noValidos = new string[4][];


		juego.taller03Ecuaciones[2].data[4].validosIzquierda[0] = new string[]{"5", "-11", "+x"};
		juego.taller03Ecuaciones[2].data[4].validosDerecha[0]   = new string[]{"6", "-",  "(-1)"};
		juego.taller03Ecuaciones[2].data[4].noValidos[0] 	   = new string[]{"1" };

		juego.taller03Ecuaciones[2].data[4].validosIzquierda[1] = new string[]{"-6", "+", "x"};
		juego.taller03Ecuaciones[2].data[4].validosDerecha[1]   = new string[]{"6", "+", "1"};
		juego.taller03Ecuaciones[2].data[4].noValidos[1] 	   = new string[]{"-"};

		juego.taller03Ecuaciones[2].data[4].validosIzquierda[2] = new string[]{"x"};
		juego.taller03Ecuaciones[2].data[4].validosDerecha[2]   = new string[]{"7", "+", "6"};
		juego.taller03Ecuaciones[2].data[4].noValidos[2] 	   = new string[]{"-", "(-6)"};

		juego.taller03Ecuaciones[2].data[4].validosIzquierda[3] = new string[]{"x"};
		juego.taller03Ecuaciones[2].data[4].validosDerecha[3]   = new string[]{"13"};
		juego.taller03Ecuaciones[2].data[4].noValidos[3] 	   = new string[]{"1", "-13"};

		//Nivel 3 - Sexta Ecuación
		/*
		x - 25 + 12 - 3 = 8 - (-9 -1)   
		x-16=8-(-10)
		x= 18+16
		x=34		
		*/
		juego.taller03Ecuaciones[2].data [5] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[2].data [5].resultado = 34f;
		juego.taller03Ecuaciones[2].data [5].planteoIzquierda = "x - 25 + 12 - 3";
		juego.taller03Ecuaciones[2].data [5].planteoDerecha = "8 - (-9 - 1)";

		juego.taller03Ecuaciones[2].data [5].validosIzquierda = new string[3][];
		juego.taller03Ecuaciones[2].data [5].validosDerecha = new string[3][];
		juego.taller03Ecuaciones[2].data [5].noValidos = new string[3][];


		juego.taller03Ecuaciones[2].data[5].validosIzquierda[0] = new string[]{"x", "-", "16"};
		juego.taller03Ecuaciones[2].data[5].validosDerecha[0]   = new string[]{"8", "-", "(-10)"};
		juego.taller03Ecuaciones[2].data[5].noValidos[0] 	   = new string[]{"+", "-8" };

		juego.taller03Ecuaciones[2].data[5].validosIzquierda[1] = new string[]{"x"};
		juego.taller03Ecuaciones[2].data[5].validosDerecha[1]   = new string[]{"18", "+", "16"};
		juego.taller03Ecuaciones[2].data[5].noValidos[1] 	   = new string[]{"(-16)"};

		juego.taller03Ecuaciones[2].data[5].validosIzquierda[2] = new string[]{"x"};
		juego.taller03Ecuaciones[2].data[5].validosDerecha[2]   = new string[]{"34"};
		juego.taller03Ecuaciones[2].data[5].noValidos[2] 	   = new string[]{"2", "-34"};

		//Nivel 3 - Séptima Ecuación
		/*
		18 - x= - 34 - 5 - (5 - 12)      
		18 - x= -34-5-(-7)
		18-x= -34-5+7 
		18-x= -39+7 
		-x = -32-18       
		-x = -50     
		 x=50		
		*/
		juego.taller03Ecuaciones[2].data [6] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[2].data [6].resultado = 50f;
		juego.taller03Ecuaciones[2].data [6].planteoIzquierda = "18 - x";
		juego.taller03Ecuaciones[2].data [6].planteoDerecha = "-34 - 5 - (5 - 12)";

		juego.taller03Ecuaciones[2].data [6].validosIzquierda = new string[5][];
		juego.taller03Ecuaciones[2].data [6].validosDerecha = new string[5][];
		juego.taller03Ecuaciones[2].data [6].noValidos = new string[5][];

		juego.taller03Ecuaciones[2].data[6].validosIzquierda[0] = new string[]{"18", "-", "x"};
		juego.taller03Ecuaciones[2].data[6].validosDerecha[0]   = new string[]{"-34", "-5", "-", "(-7)"};
		juego.taller03Ecuaciones[2].data[6].noValidos[0] 	    = new string[]{"+", "-18" };

		juego.taller03Ecuaciones[2].data[6].validosIzquierda[1] = new string[]{"18", "-", "x"};
		juego.taller03Ecuaciones[2].data[6].validosDerecha[1]   = new string[]{"-34", "-5", "+", "7"};
		juego.taller03Ecuaciones[2].data[6].noValidos[1] 	    = new string[]{"5", "-18"};

		juego.taller03Ecuaciones[2].data[6].validosIzquierda[2] = new string[]{"-", "x"};
		juego.taller03Ecuaciones[2].data[6].validosDerecha[2]   = new string[]{"-39", "+", "7", "-18"};
		juego.taller03Ecuaciones[2].data[6].noValidos[2] 	    = new string[]{"39"};
		/*
		juego.taller03Ecuaciones[2].data[6].validosIzquierda[3] = new string[]{"-x"};
		juego.taller03Ecuaciones[2].data[6].validosDerecha[3]   = new string[]{"-32", "-18"};
		juego.taller03Ecuaciones[2].data[6].noValidos[3] 	    = new string[]{"32", "-34", "+"};
*/
		juego.taller03Ecuaciones[2].data[6].validosIzquierda[3] = new string[]{"-x"};
		juego.taller03Ecuaciones[2].data[6].validosDerecha[3]   = new string[]{"-50"};
		juego.taller03Ecuaciones[2].data[6].noValidos[3] 	    = new string[]{"x", "-2"};

		juego.taller03Ecuaciones[2].data[6].validosIzquierda[4] = new string[]{"x"};
		juego.taller03Ecuaciones[2].data[6].validosDerecha[4]   = new string[]{"50"};
		juego.taller03Ecuaciones[2].data[6].noValidos[4] 	   = new string[]{"-50"};
		
		//Nivel 3 - Octava Ecuación
		/*
		-7 -7 + x = -12 - (4 - 15) + 1   
		-14 + x = -12-(-11) +1
		-14+x = -12+11+1 
		x= 0+14      
		x= 14		
		*/
		juego.taller03Ecuaciones[2].data [7] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[2].data [7].resultado = 14f;
		juego.taller03Ecuaciones[2].data [7].planteoIzquierda = "-7 -7 + x";
		juego.taller03Ecuaciones[2].data [7].planteoDerecha = "-12 - (4 - 15) +1";

		juego.taller03Ecuaciones[2].data [7].validosIzquierda = new string[4][];
		juego.taller03Ecuaciones[2].data [7].validosDerecha = new string[4][];
		juego.taller03Ecuaciones[2].data [7].noValidos = new string[4][];


		juego.taller03Ecuaciones[2].data [7].validosIzquierda[0] = new string[]{"-14" , "+", "x"};
		juego.taller03Ecuaciones[2].data [7].validosDerecha[0]   = new string[]{"-12", "-", "(-11)", "+ 1"};
		juego.taller03Ecuaciones[2].data [7].noValidos[0] 	   = new string[]{"11" };

		juego.taller03Ecuaciones[2].data [7].validosIzquierda[1] = new string[]{"-14", "+", "x"};
		juego.taller03Ecuaciones[2].data [7].validosDerecha[1]   = new string[]{"-12 ","+ 11 ", "+ 1"};
		juego.taller03Ecuaciones[2].data [7].noValidos[1] 	   = new string[]{"- 11", "- 1"};

		juego.taller03Ecuaciones[2].data [7].validosIzquierda[2] = new string[]{"x"};
		juego.taller03Ecuaciones[2].data [7].validosDerecha[2]   = new string[]{"0", "+", "14"};
		juego.taller03Ecuaciones[2].data [7].noValidos[2] 	   = new string[]{"-23"};
		
		juego.taller03Ecuaciones[2].data [7].validosIzquierda[3] = new string[]{"x"};
		juego.taller03Ecuaciones[2].data [7].validosDerecha[3]   = new string[]{"14"};
		juego.taller03Ecuaciones[2].data [7].noValidos[3] 	   = new string[]{"-14"};

		//Nivel 3 - Novena Ecuación
		/*
		-2(3x-2)=-2    
		-6x+4 =-2      
		-6x=-2-4     
		-6x=-6     
		x=-6/-6     
		x=1		
		*/

		juego.taller03Ecuaciones[2].data [8] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[2].data [8].resultado = 1f;
		juego.taller03Ecuaciones[2].data [8].planteoIzquierda = "-2(3x-2)";
		juego.taller03Ecuaciones[2].data [8].planteoDerecha = "-2";

		juego.taller03Ecuaciones[2].data [8].validosIzquierda = new string[5][];
		juego.taller03Ecuaciones[2].data [8].validosDerecha = new string[5][];
		juego.taller03Ecuaciones[2].data [8].noValidos = new string[5][];

		juego.taller03Ecuaciones[2].data [8].validosIzquierda[0] = new string[]{"-" , "6x ", "+ 4"};
		juego.taller03Ecuaciones[2].data [8].validosDerecha[0]   = new string[]{"-2"};
		juego.taller03Ecuaciones[2].data [8].noValidos[0] 	   = new string[]{"2" };

		juego.taller03Ecuaciones[2].data [8].validosIzquierda[1] = new string[]{"-", "6x"};
		juego.taller03Ecuaciones[2].data [8].validosDerecha[1]   = new string[]{"- 2 ", "- 4"};
		juego.taller03Ecuaciones[2].data [8].noValidos[1] 	   = new string[]{"+ 2 ", "+ 4"};

		juego.taller03Ecuaciones[2].data [8].validosIzquierda[2] = new string[]{"-", "6x"};
		juego.taller03Ecuaciones[2].data [8].validosDerecha[2]   = new string[]{"-6"};
		juego.taller03Ecuaciones[2].data [8].noValidos[2] 	   = new string[]{"6"};
		
		juego.taller03Ecuaciones[2].data [8].validosIzquierda[3] = new string[]{"x"};
		juego.taller03Ecuaciones[2].data [8].validosDerecha[3]   = new string[]{"(-6)",":", "(-6)"};
		juego.taller03Ecuaciones[2].data [8].noValidos[3] 	   = new string[]{".", "6"};
		
		juego.taller03Ecuaciones[2].data [8].validosIzquierda[4] = new string[]{"x"};
		juego.taller03Ecuaciones[2].data [8].validosDerecha[4]   = new string[]{"1"};
		juego.taller03Ecuaciones[2].data [8].noValidos[4] 	   = new string[]{"-1", "6"};

		//Nivel 3 - Décima Ecuación
		/*
		3x+2-4x+1+9x=6x-1          
		8x+3=6x-1         
		8x-6x=-1-3        
		2x=-4      
		x=-4/2      
		x=-2		
		*/
		
		juego.taller03Ecuaciones[2].data [9] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[2].data [9].resultado = -2f;
		juego.taller03Ecuaciones[2].data [9].planteoIzquierda = "3x+2-4x+1+9x";
		juego.taller03Ecuaciones[2].data [9].planteoDerecha = "6x-1";

		juego.taller03Ecuaciones[2].data [9].validosIzquierda = new string[5][];
		juego.taller03Ecuaciones[2].data [9].validosDerecha = new string[5][];
		juego.taller03Ecuaciones[2].data [9].noValidos = new string[5][];


		juego.taller03Ecuaciones[2].data [9].validosIzquierda[0] = new string[]{"8x" , "+", "3"};
		juego.taller03Ecuaciones[2].data [9].validosDerecha[0]   = new string[]{"6x", "-", "1"};
		juego.taller03Ecuaciones[2].data [9].noValidos[0] 	   = new string[]{"-3", "-8x" };

		juego.taller03Ecuaciones[2].data [9].validosIzquierda[1] = new string[]{"8x", "-", "6x"};
		juego.taller03Ecuaciones[2].data [9].validosDerecha[1]   = new string[]{"-1 ","- 3"};
		juego.taller03Ecuaciones[2].data [9].noValidos[1] 	   = new string[]{"+ 3"};

		juego.taller03Ecuaciones[2].data [9].validosIzquierda[2] = new string[]{"2x"};
		juego.taller03Ecuaciones[2].data [9].validosDerecha[2]   = new string[]{"-4"};
		juego.taller03Ecuaciones[2].data [9].noValidos[2] 	   = new string[]{"4", "-2x"};
	
		juego.taller03Ecuaciones[2].data [9].validosIzquierda[3] = new string[]{"x"};
		juego.taller03Ecuaciones[2].data [9].validosDerecha[3]   = new string[]{"-4 ",":", "2"};
		juego.taller03Ecuaciones[2].data [9].noValidos[3] 	   = new string[]{"(-2)", "."};

		juego.taller03Ecuaciones[2].data [9].validosIzquierda[4] = new string[]{"x"};
		juego.taller03Ecuaciones[2].data [9].validosDerecha[4]   = new string[]{"-2"};
		juego.taller03Ecuaciones[2].data [9].noValidos[4] 	   = new string[]{"2"};	

		/**
		Cuestionario
		*/
		juego.taller04Cuestionarios = new Taller04Cuestionarios[7];
		juego.taller04Cuestionarios[0] = new Taller04Cuestionarios ();
		juego.taller04Cuestionarios[0].data =  new Taller04CuestionariosData();

		juego.taller04Cuestionarios[0].data.titulo = "Diferencias entre Tierra y Marte";
		juego.taller04Cuestionarios[0].data.preguntas = new List<Pregunta> ();

		// Pregunta 1
		Pregunta preg = new Pregunta();
		preg.enunciado = "Marte y la Tierra son:";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("Planetas gaseosos", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Planetoides", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Planetas rocosos", 1));

		juego.taller04Cuestionarios[0].data.preguntas.Add (preg);

		// Pregunta 2
		preg = new Pregunta();
		preg.enunciado = "Marte está ubicado entre:";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("La Tierra y Venus", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Mercurio y la Tierra", 0) );
		preg.respuestas.Add ((new Respuesta()).set("La Tierra y Júpiter", 1));

		juego.taller04Cuestionarios[0].data.preguntas.Add (preg);


		// Pregunta 3
		preg = new Pregunta();
		preg.enunciado = "Marte y La Tierra son:";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("Iguales en tamaño", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Marte es de mayor tamaño", 0) );
		preg.respuestas.Add ((new Respuesta()).set("La Tierra es de mayor tamaño", 1));

		juego.taller04Cuestionarios[0].data.preguntas.Add (preg);


		// Pregunta 4
		preg = new Pregunta();
		preg.enunciado = "A Marte se lo conoce como:";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("El planeta azul", 0) );
		preg.respuestas.Add ((new Respuesta()).set("El planeta con anillos", 0) );
		preg.respuestas.Add ((new Respuesta()).set("El planeta rojo", 1));

		juego.taller04Cuestionarios[0].data.preguntas.Add (preg);


		// Pregunta 5
		preg = new Pregunta();
		preg.enunciado = "El nombre de Marte se debe a:";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("El dios griego de la guerra", 0) );
		preg.respuestas.Add ((new Respuesta()).set("El apellido de un astrónomo", 0) );
		preg.respuestas.Add ((new Respuesta()).set("El dios romano de la guerra", 1));

		juego.taller04Cuestionarios[0].data.preguntas.Add (preg);


		/**
		Cuestionario 2
		*/
		juego.taller04Cuestionarios[1] = new Taller04Cuestionarios();
		juego.taller04Cuestionarios[1].data =  new Taller04CuestionariosData();

		juego.taller04Cuestionarios[1].data.titulo = "Los gases";
		juego.taller04Cuestionarios[1].data.preguntas = new List<Pregunta> ();

		// Pregunta 1
		preg = new Pregunta();
		preg.enunciado = "Un gas es un estado en el cual las moléculas:";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("Forman enlaces muy fuertes", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Se separan en átomos", 0) );
		preg.respuestas.Add ((new Respuesta()).set("No forman enlaces entre sí", 1));

		juego.taller04Cuestionarios[1].data.preguntas.Add (preg);

		// Pregunta 2
		preg = new Pregunta();
		preg.enunciado = "¿Cuáles son los estados de agregación de la materia?";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("Fusión, fisión y solidificación", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Cristal, vapor y líquido", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Sólido, líquido, plasmático y gaseoso", 1));

		juego.taller04Cuestionarios[1].data.preguntas.Add (preg);

		// Pregunta 3
		preg = new Pregunta();
		preg.enunciado = "¿Cuáles son las propiedades de los gases?";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("No se comprimen", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Moléculas muy juntas", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Forma y volumen variable", 1));

		juego.taller04Cuestionarios[1].data.preguntas.Add (preg);

		// Pregunta 4
		preg = new Pregunta();
		preg.enunciado = "¿Qué son los gases nobles?";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("Un estado de la materia", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Una propiedad de los gases", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Un grupo especial de gases", 1));

		juego.taller04Cuestionarios[1].data.preguntas.Add (preg);

		// Pregunta 5
		preg = new Pregunta();
		preg.enunciado = "Propiedades de los gases nobles";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("Con olor, sin color", 0) );
		preg.respuestas.Add ((new Respuesta()).set("No conducen electricidad", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Tienen fluorescencia", 1));

		juego.taller04Cuestionarios[1].data.preguntas.Add (preg);

		/**
		Cuestionario 3
		*/
		juego.taller04Cuestionarios[2] = new Taller04Cuestionarios();
		juego.taller04Cuestionarios[2].data =  new Taller04CuestionariosData();

		juego.taller04Cuestionarios[2].data.titulo = "La gravedad";
		juego.taller04Cuestionarios[2].data.preguntas = new List<Pregunta> ();

		// Pregunta 1
		preg = new Pregunta();
		preg.enunciado = "¿Qué es la gravedad?";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("Un fenómeno artificial por el que los objetos con peso se atraen entre sí", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Un fenómeno natural por el que los objetos con peso se atraen entre sí", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Un fenómeno natural por el que los objetos con masa se atraen entre sí", 1));

		juego.taller04Cuestionarios[2].data.preguntas.Add (preg);

		// Pregunta 2
		preg = new Pregunta();
		preg.enunciado = "La ley de gravitación universal fue propuesta por";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("Gottfried Leibniz", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Robert Hooke", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Isaac Newton", 1));

		juego.taller04Cuestionarios[2].data.preguntas.Add (preg);

		// Pregunta 3
		preg = new Pregunta();
		preg.enunciado = "¿Para qué se utiliza la ley de gravitación universal?";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("Para describir los cambios de estado", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Para describir con precisión la combinación de elementos químicos", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Para describir los movimientos de cuerpos del sistema solar", 1));

		juego.taller04Cuestionarios[2].data.preguntas.Add (preg);

		// Pregunta 4
		preg = new Pregunta();
		preg.enunciado = "¿Qué se emplea para calcular esta ley?";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("El peso de dos cuerpos y la distancia entre ellos", 0) );
		preg.respuestas.Add ((new Respuesta()).set("El peso y la masa de dos cuerpos", 0) );
		preg.respuestas.Add ((new Respuesta()).set("La masa de dos cuerpos y la distancia entre ellos", 1));

		juego.taller04Cuestionarios[2].data.preguntas.Add (preg);

		// Pregunta 5
		preg = new Pregunta();
		preg.enunciado = "¿A las leyes de qué astrónomo ayudó a dar explicación?";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("Galileo Galilei", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Nicolás Copérnico", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Johannes Kepler", 1));

		juego.taller04Cuestionarios[2].data.preguntas.Add (preg);

		/**
		Cuestionario 4
		*/
		juego.taller04Cuestionarios[3] = new Taller04Cuestionarios();
		juego.taller04Cuestionarios[3].data =  new Taller04CuestionariosData();

		juego.taller04Cuestionarios[3].data.titulo = "Funcionamiento del cohete- 3º ley de Newton";
		juego.taller04Cuestionarios[3].data.preguntas = new List<Pregunta> ();

		// Pregunta 1
		preg = new Pregunta();
		preg.enunciado = "¿Quién era Isaac Newton?";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("Un físico estadounidense", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Un astrónomo australiano", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Un matemático británico", 1));

		juego.taller04Cuestionarios[3].data.preguntas.Add (preg);

		// Pregunta 2
		preg = new Pregunta();
		preg.enunciado = "Newton postuló las tres leyes de:";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("La robótica", 0) );
		preg.respuestas.Add ((new Respuesta()).set("La electricidad", 0) );
		preg.respuestas.Add ((new Respuesta()).set("La dinámica", 1));

		juego.taller04Cuestionarios[3].data.preguntas.Add (preg);

		// Pregunta 3
		preg = new Pregunta();
		preg.enunciado = "La tercera ley de Newton se refiere a:";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("La ley de movimiento continuo", 0) );
		preg.respuestas.Add ((new Respuesta()).set("La ley de gravedad", 0) );
		preg.respuestas.Add ((new Respuesta()).set("La ley de acción y reacción", 1));

		juego.taller04Cuestionarios[3].data.preguntas.Add (preg);

		// Pregunta 4
		preg = new Pregunta();
		preg.enunciado = "¿Qué se establece sobre las fuerzas de reacción y la acción?";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("Que existen en forma aislada", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Que tienen el mismo sentido", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Que actúan en pares", 1));

		juego.taller04Cuestionarios[3].data.preguntas.Add (preg);

		// Pregunta 5
		preg = new Pregunta();
		preg.enunciado = "El primer cohete fue construido por:";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("Alemania en el siglo XIX", 0) );
		preg.respuestas.Add ((new Respuesta()).set("EEUU en la primera guerra mundial", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Los chinos en 1232", 1));

		juego.taller04Cuestionarios[3].data.preguntas.Add (preg);

		/**
		Cuestionario 5
		*/
		juego.taller04Cuestionarios[4] = new Taller04Cuestionarios();
		juego.taller04Cuestionarios[4].data =  new Taller04CuestionariosData();

		juego.taller04Cuestionarios[4].data.titulo = "Movimientos de astros";
		juego.taller04Cuestionarios[4].data.preguntas = new List<Pregunta> ();

		// Pregunta 1
		preg = new Pregunta();
		preg.enunciado = "Según el modelo heliocéntrico, los planetas giran";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("Alrededor de la Tierra", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Alrededor de la Luna", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Alrededor del Sol", 1));

		juego.taller04Cuestionarios[4].data.preguntas.Add (preg);

		// Pregunta 2
		preg = new Pregunta();
		preg.enunciado = "¿Qué astro ejerce la atracción gravitacional más fuerte en el Sistema Solar?";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("La Tierra", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Los planetas", 0) );
		preg.respuestas.Add ((new Respuesta()).set("El Sol", 1));

		juego.taller04Cuestionarios[4].data.preguntas.Add (preg);

		// Pregunta 3
		preg = new Pregunta();
		preg.enunciado = "El movimiento de traslación de un planeta determina?";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("El día y la noche", 0) );
		preg.respuestas.Add ((new Respuesta()).set("El eje", 0) );
		preg.respuestas.Add ((new Respuesta()).set("El año solar", 1));

		juego.taller04Cuestionarios[4].data.preguntas.Add (preg);

		// Pregunta 4
		preg = new Pregunta();
		preg.enunciado = "El movimiento de rotación de un planeta es:";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("El giro alrededor del Sol", 0) );
		preg.respuestas.Add ((new Respuesta()).set("El giro de los satélites", 0) );
		preg.respuestas.Add ((new Respuesta()).set("El giro sobre su eje", 1));

		juego.taller04Cuestionarios[4].data.preguntas.Add (preg);

		// Pregunta 5
		preg = new Pregunta();
		preg.enunciado = "¿Qué determina la rotación?";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("El año solar", 0) );
		preg.respuestas.Add ((new Respuesta()).set("El perihelio", 0) );
		preg.respuestas.Add ((new Respuesta()).set("El día y noche", 1));

		juego.taller04Cuestionarios[4].data.preguntas.Add (preg);

		/**
		Cuestionario 6
		*/
		juego.taller04Cuestionarios[5] = new Taller04Cuestionarios();
		juego.taller04Cuestionarios[5].data =  new Taller04CuestionariosData();

		juego.taller04Cuestionarios[5].data.titulo = "Modelos atómicos";
		juego.taller04Cuestionarios[5].data.preguntas = new List<Pregunta> ();

		// Pregunta 1
		preg = new Pregunta();
		preg.enunciado = "El átomo es";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("Una partícula indivisible", 0) );
		preg.respuestas.Add ((new Respuesta()).set("La partícula más grande de un elemento químico", 0) );
		preg.respuestas.Add ((new Respuesta()).set("La partícula más pequeña de un elemento químico", 1));

		juego.taller04Cuestionarios[5].data.preguntas.Add (preg);

		// Pregunta 2
		preg = new Pregunta();
		preg.enunciado = "Las partículas que forman el núcleo del átomo son";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("Protones y electrones", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Electrones y neutrones", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Protones y neutrones", 1));

		juego.taller04Cuestionarios[5].data.preguntas.Add (preg);

		// Pregunta 3
		preg = new Pregunta();
		preg.enunciado = "Un protón tiene carga eléctrica:";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("Negativa", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Neutral", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Positiva", 1));

		juego.taller04Cuestionarios[5].data.preguntas.Add (preg);

		// Pregunta 4
		preg = new Pregunta();
		preg.enunciado = "Un electrón posee carga eléctrica";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("Positiva", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Neutral", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Negativa", 1));

		juego.taller04Cuestionarios[5].data.preguntas.Add (preg);

		// Pregunta 5
		preg = new Pregunta();
		preg.enunciado = "El neutrón se encuentra";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("En el orbital del átomo", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Dentro del protón", 0) );
		preg.respuestas.Add ((new Respuesta()).set("En el núcleo", 1));

		juego.taller04Cuestionarios[5].data.preguntas.Add (preg);
        
		/**
		Cuestionario 7
		*/
		juego.taller04Cuestionarios[6] = new Taller04Cuestionarios();
		juego.taller04Cuestionarios[6].data =  new Taller04CuestionariosData();

		juego.taller04Cuestionarios[6].data.titulo = "Magnetismo";
		juego.taller04Cuestionarios[6].data.preguntas = new List<Pregunta> ();

		// Pregunta 1
		preg = new Pregunta();
		preg.enunciado = "¿Qué es el magnetismo?";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("Una propiedad de todas las sustancias", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Una propiedad de los líquidos", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Una propiedad de algunas sustancias", 1));

		juego.taller04Cuestionarios[6].data.preguntas.Add (preg);

		// Pregunta 2
		preg = new Pregunta();
		preg.enunciado = "El primer imán conocido proviene de:";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("China", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Grecia", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Turquía", 1));

		juego.taller04Cuestionarios[6].data.preguntas.Add (preg);

		// Pregunta 3
		preg = new Pregunta();
		preg.enunciado = "Los imanes tienen:";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("Un polo único", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Alternan entre uno y dos polos", 0) );
		preg.respuestas.Add ((new Respuesta()).set("Dos polos", 1));

		juego.taller04Cuestionarios[6].data.preguntas.Add (preg);

		// Pregunta 4
		preg = new Pregunta();
		preg.enunciado = "El campo geomagnético se origina por:";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("La cantidad de hierro en el núcleo", 0) );
		preg.respuestas.Add ((new Respuesta()).set("El viento solar", 0) );
		preg.respuestas.Add ((new Respuesta()).set("La rotación terrestre y el movimiento de hierro en el núcleo", 1));

		juego.taller04Cuestionarios[6].data.preguntas.Add (preg);

		// Pregunta 5
		preg = new Pregunta();
		preg.enunciado = "Las auroras se pueden ver:";

		preg.respuestas = new List<Respuesta>();

		preg.respuestas.Add ((new Respuesta()).set("En el Polo Norte", 0) );
		preg.respuestas.Add ((new Respuesta()).set("En el Polo Sur", 0) );
		preg.respuestas.Add ((new Respuesta()).set("En las regiones polares", 1));

		juego.taller04Cuestionarios[6].data.preguntas.Add (preg);        

		/**
		 * Laboratorio 1
		*/
		juego.laboratorio01Estados = new Laboratorio01Estados();
		juego.laboratorio01Estados.data = new Laboratorio01EstadosData ();

		/**
		 * Laboratorio 2
		 */
		juego.Laboratorio02Atomo = new Laboratorio02Atomo();
		juego.Laboratorio02Atomo.data = new Laboratorio02AtomoData ();

		/**
		 * Simulador de Aterrizaje
		 */
		juego.simuladorAterrizaje = new SimuladorAterrizaje();
		juego.simuladorAterrizaje.data = new SimuladorAterrizajeData ();

		/**
		 * Simulador del Rover
		 */
		juego.simuladorRover = new SimuladorRover();
		juego.simuladorRover.data = new SimuladorRoverData ();




        //	string s = JsonUtility.ToJson (juego,true);

        //	System.IO.File.WriteAllText("Assets\\modelo\\juego2.txt",s);
        return juego;
	}

    public void recargarEstaScena() { 
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

	void OnDisable()
	{
		GameObject audioTipeo = GameObject.Find ("AudioTipeo");

		if (audioTipeo != null) {
			//GameObject.DontDestroyOnLoad (audioTipeo );
			if (audioTipeo.GetComponent<AudioSource> ().isPlaying)
				audioTipeo.GetComponent<AudioSource> ().Stop ();
		}

	}

}
