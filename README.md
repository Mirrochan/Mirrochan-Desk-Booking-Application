#  Desk Booking App

##  Prerequisites

Before starting, make sure you have installed:

- [Node.js](https://nodejs.org/)
- [Angular](https://angular.dev/) – `npm install -g @angular/cli`
- [.NET 8 SDK](https://dotnet.microsoft.com/)
- [Docker Desktop](https://www.docker.com/)
- [PostgreSQL](https://www.postgresql.org/)

---

## Running the Application using Docker

1. Clone the repository:

```bash
git clone https://github.com/your-username/your-repo.git
cd your-repo
```

2. Run the application with Docker Compose:
```bash
docker-compose up --build
```
## Configure .env
```bash
DATABASE_USERNAME=your_username
DATABASE_PASSWORD=your_password
DATABASE_NAME=your_database
DATABASE_HOST=localhost
NODE_ENV=development
```
## Running the app
```bash
cd backend
dotnet run
cd ../frontend
ng serve
```
