# 1-bosqich: Build (Vite React app'ni build qilish)
FROM node:20-alpine AS build

WORKDIR /app

# package.json va package-lock.json ni nusxalash
COPY package*.json ./

# Dependensiyalarni o'rnatish
RUN npm install

# App kodini nusxalash
COPY . .

# React app'ni build qilish (production uchun)
RUN npm run build

# 2-bosqich: Serve (build qilingan fayllarni nginx bilan serve qilish)
FROM nginx:alpine

# Build qilingan fayllarni nginx html papkasiga ko'chirish
COPY --from=build /app/dist /usr/share/nginx/html

# Custom nginx konfiguratsiyasini ko'chirish
COPY nginx.conf /etc/nginx/conf.d/default.conf

# 80-portni expose qilish
EXPOSE 80

# Nginx ishga tushirish
CMD ["nginx", "-g", "daemon off;"]
