FROM jenkinsci/jnlp-slave:3.23-1-alpine

USER root

RUN apk update && apk upgrade && apk --update add ruby && \
    apk --update add --virtual build_deps \
    build-base ruby-dev libc-dev linux-headers && \
    gem install etc --no-document && \
    gem install inspec --no-document && \
    apk del build_deps

#USER jenkins
