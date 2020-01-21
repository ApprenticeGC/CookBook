#Project Overview

[Complete Guide to Create Docker Container for Your Golang Application](https://levelup.gitconnected.com/complete-guide-to-create-docker-container-for-your-golang-application-80f3fb59a15e)

```sh
time DOCKER_BUILDKIT=1 docker image build -t gin-in-docker:latest --no-cache .

docker build . -t go-dock
docker run -p 3000:3000 go-dock

docker run -d -p 3000:3000 --rm go-dock
docker container run -it go-dock /bin/sh
```