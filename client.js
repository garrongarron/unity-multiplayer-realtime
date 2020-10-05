const net = require('net');
const client = net.createConnection({host:'192.168.1.40', port:'3000'})
client.on('data', (data)=>{
    console.log(`data: ${data}`);
})
console.log('done');