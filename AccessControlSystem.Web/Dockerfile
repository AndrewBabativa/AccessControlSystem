# Stage 1: Build the React app
FROM node:18-alpine AS build

WORKDIR /app

# Copiar package.json y package-lock.json (o yarn.lock)
COPY package*.json ./

# Instalar dependencias
RUN npm install

# Copiar el resto del código fuente
COPY . .

# Construir la app para producción
RUN npm run build

# Stage 2: Serve the React app with nginx
FROM nginx:alpine

# Copiar la build estática desde la etapa de build
COPY --from=build /app/dist /usr/share/nginx/html

# Exponer puerto 80
EXPOSE 80

# Ejecutar nginx en foreground
CMD ["nginx", "-g", "daemon off;"]
