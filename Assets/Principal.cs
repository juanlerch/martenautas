using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class Principal : MonoBehaviour {

    static string fileNameDat = "avance03.dat";

    public static string nombreActNumerosPrimos = "Numeros Primos";
    public static string nombreActCuestionarios = "Cuestionario";
    public static string nombreActEcuaciones    = "Ecuaciones";
    public static string nombreActOperacionesEnteros    = "Operaciones con números enteros";
    public static string nombreActLabEstadosMateria = "Estado de la materia";
    public static string nombreActLabEstrucutraAtomica = "Estructura Atomica";
    public static string nombreActSimRaSolar    = "Explora Sistema Solar";
    public static string nombreActSimVuelo      = "Simulador vuelo";
    public static string nombreActSimAterrisaje = "Simulador Aterrisaje";
    public static string nombreActSimExplorador = "Simulador Explorador";

    public static int avanceNoIniciada = 0;
    public static int avanceIniciada   = 1;
    public static int avanceCompleta   = 2;



    public static string user="default";

    public static Color colorOK = new Color(98f/255f, 169f/255f, 160f/255f,1f);
    public static Color colorERR= new Color(229f / 255f, 111f / 255f, 111f / 255f, 1f);

    public static Juego juego;
     
    public static Avance avance;

    public Text ui_puntaje;

    public void cambiarUsuario(GameObject usuarioTexto) {
        Text text = usuarioTexto.GetComponent<Text>();

        if (text.text == null || text.text.Trim().Length == 0)
        {
            user = "default";
        }
        else {
            user = text.text;
        }

        juego = null;
        avance = null;
        cargarModelo();

        this.cargarEscena("Main_Menu");
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
	 
	public void cargarEscena(string name){
        guardarAvance();
		SceneManager.LoadScene (name);  
	}

	public void cargarCuestionario(int nivel){
		LabTaller04Cuestionario.nivel = nivel;	
		SceneManager.LoadScene ("Cuestionario");  
	}

    public void cargarTallerEnteros(int nivel)
    {
        LabTaller02.nivel = nivel;
        SceneManager.LoadScene("Enteros");
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

	// Use this for initialization
	void Start () {
  //      VuforiaBehaviour.Instance.enabled = false;
//        VuforiaRuntime.Instance.Deinit();
        if (juego == null) {
			cargarModelo ();
		}
        LabsVuforia vuforia = GetComponent<LabsVuforia>();
        if (vuforia == null) {
            LabsNoVuforia noVuforia = GetComponent<LabsNoVuforia>();
            if (noVuforia==null) gameObject.AddComponent<LabsNoVuforia>();
        }

    }
	
	// Update is called once per frame
	void Update () {
		//Text puntaje = GameObject.Find ("puntaje").GetComponent<Text>();
		//ui_puntaje.text = "hola" ;
//		ui_puntaje.text	= ""+ juego.jugador.puntos;
	}

    public void guardarAvance() {
        if (user != null)
        {
            // string fileName = Application.persistentDataPath + "/" + "avance.dat";
            string fileName = Application.persistentDataPath + "/" + fileNameDat;
            Debug.Log("guardando avance.." + user + "->" + fileName);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(fileName);
            bf.Serialize(file, avance);
            file.Close();

            Debug.Log("guardando avance..OK " + user + "->" + fileName);

        }
        else {
            Debug.Log("No hay datos de usuario para poder guardar");
        }
        string json = JsonUtility.ToJson(avance.avanceJugadorActual(), true);
        Debug.Log("Guardando avance:"+json + "  " + avance);
        

    }

    public void iniciarAvance() {
        Debug.Log("iniciarAvance");
        string fileName = Application.persistentDataPath + "/" + fileNameDat;

        if (avance == null)
        {
            Debug.Log(fileName);
            if (File.Exists(fileName))
            {
                try
                {
                    Debug.Log("cargando..avance" + fileName);
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Open(fileName, FileMode.Open);
                    avance = (Avance)bf.Deserialize(file);
                    file.Close();
                    Debug.Log("cargando..avance OK" + fileName);

                }
                catch
                {
                    Debug.Log("error al crear avance.dat");
                }
            }
            else {
                Debug.Log("creando..avance" + fileName);
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(fileName);
                avance = new Avance();

                bf.Serialize(file, avance);
                file.Close();

                Debug.Log("creando..avance OK " + fileName);

            }
        }

        string json = JsonUtility.ToJson(avance, true);
        Debug.Log("inicializando  avance:" + json + "  " + avance.avanceJugadorActual());

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

    public Juego iniciarModelo() {
        /*if (avance == null) {
            cargarModelo();
        }*/

        Debug.Log("iniciarModelo");
        juego = new Juego();
       // juego.jugador = new Jugador();

        //juego.jugador.puntos = 10;

        //-----------------------------------------------------

        juego.taller01NumeroPrimoNivel1 = new Taller01NumeroPrimo();
        juego.taller01NumeroPrimoNivel1.data = new Taller01NumeroPrimoData[6];


        //-------------- 4[1, 2, 4]    { 3,6}
        juego.taller01NumeroPrimoNivel1.data[0] = new Taller01NumeroPrimoData();
        juego.taller01NumeroPrimoNivel1.data[0].numero = 4;
        juego.taller01NumeroPrimoNivel1.data[0].esPrimo = false;
        juego.taller01NumeroPrimoNivel1.data[0].validos = new int[] { 1, 2, 4 };
        juego.taller01NumeroPrimoNivel1.data[0].invalidos = new int[] { 3, 6 };
        //-------------- 6[1, 2, 3, 6]  { 5,8}
        juego.taller01NumeroPrimoNivel1.data[1] = new Taller01NumeroPrimoData();
        juego.taller01NumeroPrimoNivel1.data[1].numero = 6;
        juego.taller01NumeroPrimoNivel1.data[1].esPrimo = false;
        juego.taller01NumeroPrimoNivel1.data[1].validos = new int[] { 1, 2, 3, 6 };
        juego.taller01NumeroPrimoNivel1.data[1].invalidos = new int[] { 5, 8 };
        //-------------- 7[1, 7]  { 5,8}
        juego.taller01NumeroPrimoNivel1.data[2] = new Taller01NumeroPrimoData();
        juego.taller01NumeroPrimoNivel1.data[2].numero = 7;
        juego.taller01NumeroPrimoNivel1.data[2].esPrimo = true;
        juego.taller01NumeroPrimoNivel1.data[2].validos = new int[] { 1, 7 };
        juego.taller01NumeroPrimoNivel1.data[2].invalidos = new int[] { 5, 8 };


        //------------------------- 8[1, 2, 4, 8]  { 3,6,9}
        juego.taller01NumeroPrimoNivel1.data[3] = new Taller01NumeroPrimoData();
        juego.taller01NumeroPrimoNivel1.data[3].numero = 8;
        juego.taller01NumeroPrimoNivel1.data[3].esPrimo = false;
        juego.taller01NumeroPrimoNivel1.data[3].validos = new int[] { 1, 2,4,8 };
        juego.taller01NumeroPrimoNivel1.data[3].invalidos = new int[] { 3,6,9};

        //------------------------- 9[1, 3, 9]    { 2,7}
        juego.taller01NumeroPrimoNivel1.data[4] = new Taller01NumeroPrimoData();
        juego.taller01NumeroPrimoNivel1.data[4].numero = 9;
        juego.taller01NumeroPrimoNivel1.data[4].esPrimo = false;
        juego.taller01NumeroPrimoNivel1.data[4].validos = new int[] { 1, 3,9 };
        juego.taller01NumeroPrimoNivel1.data[4].invalidos = new int[] { 2,7 };


        //------------------------- 10[1, 2, 5, 10] { 3,6,7}
        juego.taller01NumeroPrimoNivel1.data[5] = new Taller01NumeroPrimoData();
        juego.taller01NumeroPrimoNivel1.data[5].numero = 10;
        juego.taller01NumeroPrimoNivel1.data[5].esPrimo = false;
        juego.taller01NumeroPrimoNivel1.data[5].validos = new int[] { 1, 2, 5, 10 };
        juego.taller01NumeroPrimoNivel1.data[5].invalidos = new int[] { 3, 6, 7 };

        //-------------------------
        //20[1, 2, 4, 5, 10, 20] { 8,40,-2,3}


        int puntaje = 0;
        foreach (Taller01NumeroPrimoData a in juego.taller01NumeroPrimoNivel1.data)
        {
            puntaje += a.puntoOK;
        }
        juego.taller01NumeroPrimoNivel1.puntajeMaximo = puntaje;
        //  juego.taller01NumeroPrimoNivel1.puntajeRecord = avance.puntaje(user, "numerosPrimos", 1);



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
        juego.taller02NumerosNaturales[2].data[0].operacion = "-10 ∧ 3 ";
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

        // 4 ∧ 3 = 12, 24, -12, 64
        juego.taller02NumerosNaturales[2].data[4].operacion = "4 ^ 3";
        juego.taller02NumerosNaturales[2].data[4].resultados.Add("64");
        juego.taller02NumerosNaturales[2].data[4].addOpciones(new int[] { 12, 24, -12, 64 });

        // 7 ∧ 2 = 128, 14, 49, 7
        juego.taller02NumerosNaturales[2].data[5].operacion = "7 ^ 2";
        juego.taller02NumerosNaturales[2].data[5].resultados.Add("49");
        juego.taller02NumerosNaturales[2].data[5].addOpciones(new int[] { 128, 14, 49, 7 });

        puntaje = 0;
        foreach (Taller02NumerosNaturalesData a in juego.taller02NumerosNaturales[1].data)
        {
            puntaje += a.puntoOK;
        }
        juego.taller02NumerosNaturales[2].puntajeMaximo = puntaje;




        //---------------------------------------------------------------------------------------
        /*Ecuaciones*/
        juego.taller03Ecuaciones = new Taller03Ecuaciones[3];
		juego.taller03Ecuaciones[0] = new Taller03Ecuaciones();
		juego.taller03Ecuaciones[0].data =  new Taller03EcuacionesData[2]; 

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
		juego.taller03Ecuaciones[0].data [1].resultado = 4f;
		juego.taller03Ecuaciones[0].data [1].planteoIzquierda = "3x + 6";
		juego.taller03Ecuaciones[0].data [1].planteoDerecha = "18";

		juego.taller03Ecuaciones[0].data [1].validosIzquierda = new string[4][];
		juego.taller03Ecuaciones[0].data [1].validosDerecha = new string[4][];
		juego.taller03Ecuaciones[0].data [1].noValidos = new string[4][];


		juego.taller03Ecuaciones[0].data[1].validosIzquierda[0] = new string[]{"3x"};
		juego.taller03Ecuaciones[0].data[1].validosDerecha[0]   = new string[]{"18", "-6"};
		juego.taller03Ecuaciones[0].data [1].noValidos[0] 	   = new string[]{"+6", "9x" };

		juego.taller03Ecuaciones[0].data[1].validosIzquierda[1] = new string[]{"3x"};
		juego.taller03Ecuaciones[0].data[1].validosDerecha[1]   = new string[]{"12"};
		juego.taller03Ecuaciones[0].data[1].noValidos[1] 	   = new string[]{"x", "24"};

		juego.taller03Ecuaciones[0].data[1].validosIzquierda[2] = new string[]{"x"};
		juego.taller03Ecuaciones[0].data[1].validosDerecha[2]   = new string[]{"12", ":", "3"};
		juego.taller03Ecuaciones[0].data[1].noValidos[2] 	   = new string[]{"18", "."};

		juego.taller03Ecuaciones[0].data[1].validosIzquierda[3] = new string[]{"x"};
		juego.taller03Ecuaciones[0].data[1].validosDerecha[3]   = new string[]{"4"};
		juego.taller03Ecuaciones[0].data[1].noValidos[3] 	   = new string[]{"36"};


		//Nivel 2 - Primera Ecuación
		juego.taller03Ecuaciones[1] = new Taller03Ecuaciones();
		juego.taller03Ecuaciones[1].data =  new Taller03EcuacionesData[1]; 

		juego.taller03Ecuaciones[1].data [0] = new Taller03EcuacionesData ();
		juego.taller03Ecuaciones[1].data [0].resultado = -15f;
		juego.taller03Ecuaciones[1].data [0].planteoIzquierda = "7x + 5";
		juego.taller03Ecuaciones[1].data [0].planteoDerecha = "-100";

		juego.taller03Ecuaciones[1].data [0].validosIzquierda = new string[4][];
		juego.taller03Ecuaciones[1].data [0].validosDerecha = new string[4][];
		juego.taller03Ecuaciones[1].data [0].noValidos = new string[4][];


		juego.taller03Ecuaciones[1].data[0].validosIzquierda[0] = new string[]{"7x"};
		juego.taller03Ecuaciones[1].data[0].validosDerecha[0]   = new string[]{"-100", "-5"};
		juego.taller03Ecuaciones[1].data [0].noValidos[0] 	   = new string[]{"100", "+5" };

		juego.taller03Ecuaciones[1].data[0].validosIzquierda[1] = new string[]{"7x"};
		juego.taller03Ecuaciones[1].data[0].validosDerecha[1]   = new string[]{"-105"};
		juego.taller03Ecuaciones[1].data[0].noValidos[1] 	   = new string[]{"-95", "105"};

		juego.taller03Ecuaciones[1].data[0].validosIzquierda[2] = new string[]{"x"};
		juego.taller03Ecuaciones[1].data[0].validosDerecha[2]   = new string[]{"-105",":","7"};
		juego.taller03Ecuaciones[1].data[0].noValidos[2] 	   = new string[]{".", "-7"};

		juego.taller03Ecuaciones[1].data[0].validosIzquierda[3] = new string[]{"x"};
		juego.taller03Ecuaciones[1].data[0].validosDerecha[3]   = new string[]{"-15"};
		juego.taller03Ecuaciones[1].data[0].noValidos[3] 	   = new string[]{"15", "-20"};


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
		 * Laboratorio 1
		*/
		juego.laboratorio01Estados = new Laboratorio01Estados();
		juego.laboratorio01Estados.data = new Laboratorio01EstadosData ();

        //	string s = JsonUtility.ToJson (juego,true);

        //	System.IO.File.WriteAllText("Assets\\modelo\\juego2.txt",s);
        return juego;
	}

    public void recargarEstaScena() { 
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
