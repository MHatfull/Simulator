FROM ubuntu
RUN mkdir /src
ADD Builds/Linux/ /src
EXPOSE 4444
CMD ./src/server.x86_64 -logfile output.log & touch output.log ; tail -f output.log
