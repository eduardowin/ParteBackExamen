# Examen Backend C#

## Descripción

Desarrollar una solución(C#) para lanzar una serie de drones para sobrevolar el territorio y
detectar fuentes de calor.

## Herramientas utilizadas
  - Visual Studio 2019 (Community)
  - .Net Core 3.1
  - Lenguaje C#
  - Swashbuckle.AspNetCore 6.1.4

## Estructura prototipo inicial
![image](https://user-images.githubusercontent.com/10048889/120905061-3a520a00-c615-11eb-892a-fa4d3abc1139.png)

## Estructura Prototipo y consideraciones finales

- Definir formula de avance del dron al volar, este puede moverse hacia el norte, sur, este y oeste segun el grafico:
  ![image](https://user-images.githubusercontent.com/10048889/120905620-e21d0700-c618-11eb-9998-68781e411aba.png)

- Definir dirección al girar hacia la izquierda "L" o hacia la derecha "R"
  ![image](https://user-images.githubusercontent.com/10048889/120905863-a97e2d00-c61a-11eb-8087-5b58280d1827.png)
  Ejemplo:
  Si la direccion inicial está en "este" eso equivale a 0° al girar hacia la izquierda se le aumentan 90°, el resultado de la suma es 90° eso indica que la nueva direccion    es "norte".
  Si la dirección inicial es "oeste" eso equivale a 180° o -180°(segun la grafica), al girar hacia la derecha se le disminuye -90°, el resultado de la operación seria 90° y -270° respectivamente, eso indica que la nueva direccion seria "norte".

## Uso instrucciones

  1 Ejecutar la aplicacion con el programa visual studio 2019(Community), donde previamente se le haya instalado .net Core 3.1.
  2 La aplicacion tipo Api, levantará con el puerto 44392 y a traves del endpoint /api/ControlForestal (POST) se enviará las instrucciones por el body de la peticcion http, de las siguientes 2 formas:
    - Con postman:
      - Copiar y pegar la url "https://localhost:44392/api/ControlForestal" del Api.
          ![image](https://user-images.githubusercontent.com/10048889/120906472-36c38080-c61f-11eb-8f89-f2dbe58da22d.png)
      - Pegar la siguiente estructura en el body:
        
          ```
          {
              "PerimetroRectanguloBase": 5,
              "PerimetroRectanguloAltura": 5,
              "InstruccionesDto": [
                  {
                      "CoordenadaVuelo": {
                          "PuntoX": 3,
                          "PuntoY": 3,
                          "Direccion" :"E"
                      },
                      "Acciones": ["L"]
                  },
                  {
                      "CoordenadaVuelo": {
                          "PuntoX": 3,
                          "PuntoY": 3,
                          "Direccion" :"E"
                      },
                      "Acciones": ["M","M","R","M","M","R","M","R","R","M"]
                  },
                  {
                      "CoordenadaVuelo": {
                          "PuntoX": 1,
                          "PuntoY": 2,
                          "Direccion" :"N"
                      },
                      "Acciones": ["L","M","L","M","L","M","L","M","M","L","M","L","M","L","M","L","M","M"]
                  }
              ] 
          } 
         ```
   -  Enviar la petición con el boton "Send", el resultado respondera un code status 200 y la respuesta final con las coordenadas del recorrido del dron.
          ```
          [
              {
                  "puntoX": 3,
                  "puntoY": 3,
                  "direccion": "N"
              },
              {
                  "puntoX": 5,
                  "puntoY": 1,
                  "direccion": "E"
              },
              {
                  "puntoX": 1,
                  "puntoY": 4,
                  "direccion": "N"
              }
          ]
          ```
          ![image](https://user-images.githubusercontent.com/10048889/120906501-8d30bf00-c61f-11eb-9299-b3ee335656c2.png)
          ![image](https://user-images.githubusercontent.com/10048889/120906519-bd785d80-c61f-11eb-9e41-813fa61c666b.png)
    - Con swagger:
      - Ir la url https://localhost:44392/swagger/index.html y click en el endpoint POST "/api/ControlForestal"
        ![image](https://user-images.githubusercontent.com/10048889/120906822-3aa4d200-c622-11eb-866c-d1b022e5d991.png)
      - Click en Try it out
        ![image](https://user-images.githubusercontent.com/10048889/120906856-7dff4080-c622-11eb-9af1-237d643045fd.png)
      - Pegar la siguiente estructura en el body:
          ```
          {
              "PerimetroRectanguloBase": 5,
              "PerimetroRectanguloAltura": 5,
              "InstruccionesDto": [
                  {
                      "CoordenadaVuelo": {
                          "PuntoX": 3,
                          "PuntoY": 3,
                          "Direccion" :"E"
                      },
                      "Acciones": ["L"]
                  },
                  {
                      "CoordenadaVuelo": {
                          "PuntoX": 3,
                          "PuntoY": 3,
                          "Direccion" :"E"
                      },
                      "Acciones": ["M","M","R","M","M","R","M","R","R","M"]
                  },
                  {
                      "CoordenadaVuelo": {
                          "PuntoX": 1,
                          "PuntoY": 2,
                          "Direccion" :"N"
                      },
                      "Acciones": ["L","M","L","M","L","M","L","M","M","L","M","L","M","L","M","L","M","M"]
                  }
              ] 
          } ```
          ![image](https://user-images.githubusercontent.com/10048889/120906993-6f655900-c623-11eb-961c-20f1a6742abc.png)
        -  Enviar la petición con el boton "Execute", el resultado respondera un code status 200 y la respuesta final con las coordenadas del recorrido del dron.
            ![image](https://user-images.githubusercontent.com/10048889/120907008-915edb80-c623-11eb-8f77-740ec70b62c6.png)


