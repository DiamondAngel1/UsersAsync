# Етап 1: збірка проєкту
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копіюємо файл проєкту і відновлюємо залежності
COPY MyMvcApp.csproj ./
RUN dotnet restore

# Копіюємо увесь код і збираємо
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Етап 2: рантайм (що буде запущено на сервері)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish ./

# Запускаємо сайт
ENTRYPOINT ["dotnet", "MyMvcApp.dll"]