# Build Phase
FROM node:8.9.1
WORKDIR /app

# Copy dependency manifests
COPY ./cotb-web/package*.json ./cotb-web/
COPY ./cotb-client/package*.json ./cotb-client/
COPY ./cotb-server/package*.json ./cotb-server/

# Install dependencies and cache
RUN  npm i --prefix ./cotb-web && \
     npm i --prefix ./cotb-client && \
     npm i --prefix ./cotb-server

# Copy src
COPY ./cotb-web ./cotb-web
COPY ./cotb-client ./cotb-client
COPY ./cotb-server ./cotb-server

# Build Web
RUN npm run build --prefix ./cotb-web && \
    cp -R ./cotb-web/build ./cotb-server/public/app

# Execute app
WORKDIR /app/cotb-server
CMD ["npm", "run", "start:release"]
EXPOSE 80:80