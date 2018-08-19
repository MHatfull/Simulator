FROM scratch
ADD Builds/Linux/ /

RUN chown +x Linux/server.x86_64

CMD ["./Linux/server.x86_64"]
