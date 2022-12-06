
namespace CRUDAPI.Utils
{

   public class CalculoImc
   {


      public static double CalcularImc(double peso, double altura ){

        double imc= 1;

        double taltura = 0.0;

       taltura = (altura*altura);

        imc =  peso / taltura;

 
      return imc;       
    }


    public static string classificar(double imc){

       string classific = "x";


         if(imc < 18.5){

           classific = "MAGREZA";


         }else if((imc >= 18.5) && (imc < 24.9)){

           
           classific = "NORMAL";

         }else if((imc >= 25.0) && (imc < 29.9)){


           classific = "SOBREPESO";

         }else if((imc >= 30.0) && (imc < 39.9) ){

           
           classific = "OBESIDADE"; 


         }else if(imc >= 40.0 ){


          classific = "OBESIDADE GRAVE";
      

         }



        return classific;
    }



  }


}

