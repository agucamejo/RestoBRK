# RestoBRK - Gestión Administrativa de Restaurante con ASP.NET Core MVC, Bootstrap 5 y OAuth con Google

¡Bienvenido al repositorio de RestoBRK! Este proyecto ofrece una solución completa para la gestión administrativa de un restaurante, desarrollada con ASP.NET Core MVC, Entity Framework, C# en .Net Framework, y utiliza SQL Server como base de datos. RestoBRK está diseñado para facilitar la administración de platos, bebidas, medios de pago, lista de precios, promociones y pedidos (take away, delivery y mesas), con un diseño moderno gracias a Bootstrap 5. Además, cuenta con autenticación OAuth a través de Google para una experiencia de inicio de sesión y registro segura y rápida.

## Características Destacadas

- **ASP.NET Core MVC:** RestoBRK utiliza ASP.NET Core MVC para construir una aplicación web robusta y escalable. La arquitectura MVC proporciona una separación clara entre la lógica de presentación, la lógica de negocio y el almacenamiento de datos.

- **Entity Framework:** La gestión de datos se realiza a través de Entity Framework, lo que permite una fácil interacción con la base de datos SQL Server. Puedes beneficiarte de modelos de datos sólidos y consultas LINQ para acceder y manipular la información de la base de datos.

- **Bootstrap 5:** El diseño moderno y responsivo de RestoBRK se logra gracias a Bootstrap 5, la popular biblioteca de diseño front-end. Disfruta de componentes visuales atractivos y una experiencia de usuario intuitiva.

- **OAuth con Google:** RestoBRK implementa la autenticación OAuth con Google para proporcionar un inicio de sesión seguro y conveniente para los usuarios. Esto permite a los usuarios utilizar sus cuentas de Google para acceder a la aplicación.

- **Gestión de Platos, Bebidas y Medios de Pago:** RestoBRK proporciona funcionalidades específicas para gestionar platos, bebidas y medios de pago. Mantén un registro claro de los productos disponibles y los métodos de pago aceptados.

- **Lista de Precios y Promociones:** Administra la lista de precios de tus productos y configura promociones para atraer a tus clientes. Esta característica te permite ajustar dinámicamente los precios y ofrecer descuentos especiales.

- **Gestión de Pedidos:** RestoBRK facilita la gestión de pedidos, ya sea para llevar (take away), entrega a domicilio (delivery) o pedidos en mesas. Realiza un seguimiento de los pedidos en curso y mejora la eficiencia de tu servicio.

## Cómo empezar

Si deseas explorar y trabajar con RestoBRK en tu entorno local, sigue estos pasos:

1. **Clona el Repositorio:** Ejecuta `git clone https://github.com/tuusuario/restobrk.git` en tu terminal.
2. **Configura la Base de Datos:** Abre el archivo `appsettings.json` y configura la cadena de conexión para tu instancia de SQL Server.
3. **Configura OAuth con Google:** También en el archivo `appsettings.json`, configura las credenciales necesarias de tu aplicación Google en la sección de autenticación OAuth.
4. **Instala las Dependencias:** Asegúrate de tener las herramientas de desarrollo de .NET Core instaladas. Luego, navega al directorio del proyecto (`cd restobrk`) y ejecuta `dotnet restore` para instalar todas las dependencias necesarias.
5. **Configura la Migración:** Si estás utilizando Code First, ejecuta `dotnet ef migrations add InitialMigration` para crear la primera migración. Asegúrate de que el modelo se haya guardado y actualizado correctamente.
6. **Aplica la Migración:** Ejecuta `dotnet ef database update` para aplicar la migración y crear la base de datos.
7. **Inicia la Aplicación:** Utiliza `dotnet run` para iniciar la aplicación y accede a ella desde tu navegador en `https://localhost:5001`.

## Contribuciones

¡Las contribuciones a RestoBRK son bienvenidas! Si deseas contribuir, realiza un fork del repositorio, crea una rama para tus cambios y envía un pull request. Agradecemos cualquier aporte para mejorar este proyecto y hacerlo aún más funcional y eficiente para la gestión de restaurantes.
