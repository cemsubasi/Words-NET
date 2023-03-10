FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /app
COPY . .
RUN dotnet restore
# RUN dotnet dev-certs https --clean --import ./certificate.pfx -p parola1==-
RUN dotnet dev-certs https -ep ./certificate.pfx -p parola1==-
RUN dotnet publish -o /app/published-app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
# COPY --from=build /certificate.pfx /app/certificate.pfx
COPY --from=build /app/published-app /app
ENTRYPOINT [ "dotnet", "/app/Presentation.dll"  ]
