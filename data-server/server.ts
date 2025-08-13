import * as net from 'net';
import mysql from 'mysql2/promise';

// Remote DB connection pool
const db = mysql.createPool({
    host: 'ftp.stylianop.gr',     
    port: 3306,                   
    user: 'geosty',         
    password: '$%APQnn7jj9oui5s', 
    database: 'SessionsFinder',     
    waitForConnections: true,
    connectionLimit: 5,
    queueLimit: 0
});

const server = net.createServer((socket) => {
    console.log('C# client connected');

    socket.on('data', async (data: Buffer) => {
        const message = data.toString('utf-8').trim();
        console.log(`[Client] ${message}`);
    
        const [command, username, password] = message.split('|');
    
        console.log("Before login")

        if (command.toLowerCase() === 'login') {
            try {
                const [rows] = await db.query(
                    'SELECT * FROM users WHERE username = ? AND password = ?',
                    [username, password]
                );
    
                if ((rows as any[]).length > 0) {
                    // Optional: send all user data
                    const [allUsers] = await db.query('SELECT * FROM users');
                    const userList = (allUsers as any[]).map(user => {
                        return `${user.id},${user.username},${user.password},${user.email}`;
                    }).join('\n');
    
                    socket.write(`Login successful\n${userList}\n`);
                } else {
                    socket.write('Login failed\n');
                }
            } catch (err) {
                console.error('DB error:', err);
                socket.write('Internal server error\n');
            }
    
        } else if (command.toLowerCase() === 'signin') {
            try {
                await db.query(
                    'INSERT INTO users (username, password) VALUES (?, ?)',
                    [username, password]
                );
                socket.write('Signup successful\n');
            } catch (err) {
                console.error('DB error:', err);
                socket.write('Signup failed\n');
            }
    
        } else {
            socket.write(`Unknown command: ${command}\n`);
        }
    });
    

    socket.on('end', () => console.log('C# client disconnected'));
    socket.on('error', (err) => console.error('Socket error:', err.message));
}).on('error', (err) => {
    console.log('Server error', err);
});

// Start listening
server.listen(9000, '127.0.0.1', () => {
    console.log('TCP server listening on 127.0.0.1:9000');
});
