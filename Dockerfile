FROM ubuntu
ADD Builds/Linux/ /
EXPOSE 4444
CMD ./Linux/server.x86_64 -logfile output.log & touch output.log ; tail -f output.log
