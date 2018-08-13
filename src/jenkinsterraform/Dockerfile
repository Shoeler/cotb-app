FROM jenkinsci/jnlp-slave:3.23-1-alpine

USER root

ENV TERRAFORM_VERSION=0.11.7
ENV TF_FILE=terraform_${TERRAFORM_VERSION}_linux_amd64.zip

RUN apk add --update curl unzip \
    && curl -O https://releases.hashicorp.com/terraform/$TERRAFORM_VERSION/${TF_FILE} \
    && unzip ${TF_FILE} -d /usr/bin && rm -f ${TF_FILE} \
    && apk del unzip curl