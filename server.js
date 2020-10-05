const net = require('net')
const PORT = 3000
var servidor = net.createServer(function (socket) {
    socket.write(Buffer.from("conectado", "utf-8"));
    socket.on("close", function () {
        console.log("DESCONECTADO");
    });
})
servidor.listen(PORT, function () {
    console.log("CORRIENDO EN EL PUERTO " + PORT);
    console.log("Esperando jugadores que busquen partida...");
})
