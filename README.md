# Movies Test

Aplicación de catálogo de **películas** y **directores** con CRUD completo, construida como prueba técnica. Backend en ASP.NET Core (.NET 10) con MySQL, y frontend en Vue 3.

---

# Video Demo

https://github.com/user-attachments/assets/16fe5696-75a1-420b-a5df-a94956a1b1a0

## Stack

**Backend**
- ASP.NET Core (.NET 10) — API REST versionada (`/api/v1`)
- Entity Framework Core + [Pomelo](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql) (proveedor MySQL)
- Arquitectura Controllers → Services → DbContext
- Manejo global de excepciones (`IExceptionHandler` + ProblemDetails)
- OpenAPI / Swagger UI (NSwag) en desarrollo

**Frontend**
- Vue 3 (`<script setup>`) + Vite
- Vue Router
- TanStack Query (`@tanstack/vue-query`) para data-fetching y cache
- `motion-v` para animaciones (morph, layout, entrada/salida)
- Axios

**Base de datos**
- MySQL — base `Movies_DB`, tablas `Directors` y `Movies`

---

## Características

- **CRUD de Películas** — alta, edición, borrado (lógico) y listado.
- **CRUD de Directores** — alta, edición, borrado (lógico) y listado.
- **Borrado lógico (soft delete)** — los registros no se eliminan físicamente; se marcan como inactivos.
- **Búsqueda en vivo** — filtrado client-side por nombre, género/nacionalidad, etc.
- **UI animada** — barra de búsqueda que se transforma, reflujo de la lista, y transiciones de entrada/salida de las tarjetas.

---

## Requisitos previos

- [.NET SDK 10](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) 20+ y npm
- [MySQL](https://www.mysql.com/) 8+ en ejecución (puerto `3306`)

---

## Puesta en marcha

### 1. Base de datos

Ejecuta primero el esquema y luego los datos de ejemplo:

```bash
mysql -u root -p < database/schema.sql
mysql -u root -p < database/seed.sql
```

- `schema.sql` crea la base `Movies_DB` y las tablas.
- `seed.sql` inserta directores y películas de ejemplo (no borra tablas, se puede re-ejecutar de forma controlada).

### 2. Backend

Configura la cadena de conexión con tus credenciales de MySQL en `backend/appsettings.Development.json` (el entorno por defecto es `Development`):

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Port=3306;Database=Movies_DB;User=root;Password=TU_PASSWORD;"
}
```

Levanta la API:

```bash
dotnet run --project backend
```

- API: `http://localhost:5084/api/v1`
- Swagger UI (solo en desarrollo): `http://localhost:5084/swagger`

### 3. Frontend

```bash
cd frontend
npm install
npm run dev
```

- App: `http://localhost:5173`

> El backend habilita CORS para `http://localhost:5173`. Si cambias el puerto del frontend, actualiza la política CORS en `backend/Program.cs`.

## Decisiones de diseño

- **Borrado lógico, no físico.** Tanto directores como películas usan un campo `Active`. Borrar = `Active = false`. Los listados (`GET`) filtran los inactivos en el backend.
- **Integridad al asignar director.** Al crear una película (o al reasignar su director), el director debe estar **activo**. Sin embargo, editar otros campos de una película cuyo director fue dado de baja **no** se bloquea: se conserva su director original y el formulario lo muestra marcado como *(Inactive)*. Así el borrado de un director nunca deja películas ineditables.
- **Búsqueda en el cliente.** El catálogo se carga completo y se cachea con TanStack Query; el filtrado ocurre en memoria.
- **Componentes de presentación compartidos.** `EntityCard` y `EntityList` son agnósticos de la entidad, Películas y Directores solo aportan el contenido de la tarjeta y sus acciones. Evita duplicar la lógica de presentación entre secciones.

---

## API

Base URL: `http://localhost:5084/api/v1`

| Método | Ruta               | Descripción                         |
|--------|--------------------|-------------------------------------|
| GET    | `/movies`          | Lista películas activas             |
| GET    | `/movies/{id}`     | Obtiene una película                |
| POST   | `/movies`          | Crea una película                   |
| PUT    | `/movies/{id}`     | Actualiza una película              |
| DELETE | `/movies/{id}`     | Borrado lógico de una película      |
| GET    | `/directors`       | Lista directores activos            |
| GET    | `/directors/{id}`  | Obtiene un director                 |
| POST   | `/directors`       | Crea un director                    |
| PUT    | `/directors/{id}`  | Actualiza un director               |
| DELETE | `/directors/{id}`  | Borrado lógico de un director       |
