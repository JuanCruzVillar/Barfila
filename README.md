# Barfila 
App para registrar y puntuar películas, porque toda buena peli tiene que ser recordada!

## ¿Por qué existe Barfila?

Mi novia es cinéfila, diria que es su mayor hobby. Ve películas de todo tipo y siempre le gusto dar su opinion de las mismas. El problema era que no tenía un lugar propio donde guardarlas, mas que compartiendolas en charlas con amigos. Barfila nacio para eso, para que pueda registrar cada película que ve, escribir lo que piensa y descubrir que ver después basándose en su propio gusto. El sistema analiza las películas que el usuario rateo con 7 o más, extrae sus géneros y directores que son mas frecuentes, y consulta películas similares en TMDB. Cada pelicula recibe un score propio:

- +2 puntos por cada género que coincida con los favoritos del usuario
- +3 puntos por cada director que coincida

Las 10 mejor rankeadas se devuelven como recomendaciones y se cachean en Redis por 24 horas para no repetir el proceso en cada consulta.
Es un motor de recomendaciones simple, pero efectivo. 

## Stack

### Backend
- ASP.NET Core (.NET 8)
- Clean Architecture + CQRS con MediatR
- SQL Server + Entity Framework Core
- Redis
- JWT Authentication
- TMDB API (The Movie Database) — datos de películas, búsqueda y recomendaciones
- Docker + Docker Compose

### Frontend (próximamente)
- React + TypeScript
- En construccion una vez finalizado el backend

## Arquitectura

El proyecto aplica Clean Architecture con separacion en capas:


Domain        →   entidades
Application   →   casos de uso, interfaces, DTOs
Infrastructure →   EF Core, Redis, TMDB, JWT
Barfila (API) →   controllers, middleware


Los controllers no tienen logica, solo reciben y responden. Cada caso de uso tiene su command o query con su handler, para emplear de manera correcta el patron CQRS.


## Como levantar el proyecto

1. Clona el repo
2. Crea una cuenta en [TMDB](https://www.themoviedb.org/) y obtené tu API key
3. Copiá `.env.example` a `.env` y completá las variables
4. Desde la carpeta `docker/`:

```bash
docker-compose --env-file ../.env up --build
```


