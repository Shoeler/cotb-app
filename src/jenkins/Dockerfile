FROM jenkins/jenkins:lts

EXPOSE 8080 50000

USER root

WORKDIR /usr/local/bin

RUN apt-get update -qq && apt-get install -y -qq --no-install-recommends \
    make \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/* /tmp/* /var/tmp/*

RUN curl -fsSL get.docker.com -o get-docker.sh && sh get-docker.sh && usermod -aG docker jenkins

RUN curl -L https://github.com/docker/compose/releases/download/1.22.0/docker-compose-$(uname -s)-$(uname -m) -o /usr/local/bin/docker-compose \
    && chmod +x /usr/local/bin/docker-compose

RUN curl -L https://storage.googleapis.com/kubernetes-release/release/v1.11.1/bin/linux/amd64/kubectl -o /usr/local/bin/kubectl \
    && chmod +x /usr/local/bin/kubectl

WORKDIR $JENKINS_HOME