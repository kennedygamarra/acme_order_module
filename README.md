# ACME API REST

**NOTA:** Se han implementado dos endpoint debido a que el endpoint suministrado en la prueba (`https://run.mocky.io/v3/19217075-6d4e-4818-98bc-416d1feb7b84`) no está disponible. Por lo que (`api/v1/Order/ShipOrder/`) apunta al anterior mencionado y (`api/v1/Order/ShipOrderPrueba/`) se usa con datos de prueba que se han proporcionado para demostrar el funcionamiento del servicio.


Implementar un servicio a través de una API REST para el ciclo de abastecimiento donde se debe enviar la información de los pedidos a través de esta API con mensajería JSON, para que de esta forma el servicio le retorne información del sistema de envío de pedidos.

## Descripción

Este proyecto expone una API REST que realiza la transformación de datos de JSON a XML y viceversa, utilizando un mapeo específico para cada caso. El proyecto está preparado para ejecutarse en contenedores Docker.

## Funcionalidades

- **Exposición de API REST con mensajería JSON**.
- **Transformación de la petición de JSON a XML** de acuerdo con la siguiente tabla de mapeo:

    | REST (JSON)       | SOAP (XML)   | Datos de Prueba                 |
    |-------------------|--------------|----------------------------------|
    | numPedido         | pedido       | 75630275                        |
    | cantidadPedido    | Cantidad     | 1                               |
    | codigoEAN         | EAN          | 00110000765191002104587         |
    | nombreProducto    | Producto     | Armario INVAL                   |
    | numDocumento      | Cedula       | 1113987400                      |
    | direccion         | Direccion    | CR 72B 45 12 APT 301            |

- **Transformación de la respuesta de XML a JSON** de acuerdo con la siguiente tabla de mapeo:

    | SOAP (XML)        | REST (JSON)  | Datos de Prueba                  |
    |-------------------|--------------|----------------------------------|
    | Codigo            | codigoEnvio  | 80375472                         |
    | Mensaje           | estado       | Entregado exitosamente al cliente|

- **Endpoint de prueba**: `https://run.mocky.io/v3/19217075-6d4e-4818-98bc-416d1feb7b84`

## Instalación

Para ejecutar este proyecto en tu máquina local, sigue los siguientes pasos:

1. Clona el repositorio:
    ```bash
    git clone git@github.com:kennedygamarra/acme_order_module.git
    cd acme_order_module
    ```

2. Construye la imagen de Docker:
    ```bash
    docker build -f src/acme.order.api/Dockerfile --force-rm -t acme-api .
    ```

3. Ejecuta el contenedor de Docker:
    ```bash
    docker run -p 8080:8080 acme-api
    ```

## Uso

Para enviar una solicitud a la API, puedes utilizar `curl` o Postman. Aquí hay un ejemplo utilizando `curl`:

```bash
curl -X POST http://localhost:8080/api/v1/Order/ShipOrder/ -H "Content-Type: application/json" -d '{
	"enviarPedido": {
		"numPedido": "75630275",
		"cantidadPedido": "1",
		"codigoEAN": "00110000765191002104587",
		"nombreProducto": "Armario INVAL",
		"numDocumento": "1113987400",
		"direccion": "CR 72B 45 12 APT 301"
	}
}'

