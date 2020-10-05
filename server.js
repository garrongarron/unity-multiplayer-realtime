const net = require('net')
const PORT = 3000
var servidor = net.createServer(function (socket) {
    socket.write(Buffer.from("connected (Message from SERVER)", "utf-8"));
    socket.on("close", function () {
        console.log("Client disconnected");
    });
    socket.on("data", function (data) {
        console.log(data.toString());
    });
    console.log('Conection created...');
})
servidor.listen(PORT, function () {
    console.log("Running on PORT " + PORT);
    console.log("Waiting for conections...");
})
