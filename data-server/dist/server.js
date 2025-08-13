"use strict";
var __createBinding = (this && this.__createBinding) || (Object.create ? (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    var desc = Object.getOwnPropertyDescriptor(m, k);
    if (!desc || ("get" in desc ? !m.__esModule : desc.writable || desc.configurable)) {
      desc = { enumerable: true, get: function() { return m[k]; } };
    }
    Object.defineProperty(o, k2, desc);
}) : (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    o[k2] = m[k];
}));
var __setModuleDefault = (this && this.__setModuleDefault) || (Object.create ? (function(o, v) {
    Object.defineProperty(o, "default", { enumerable: true, value: v });
}) : function(o, v) {
    o["default"] = v;
});
var __importStar = (this && this.__importStar) || (function () {
    var ownKeys = function(o) {
        ownKeys = Object.getOwnPropertyNames || function (o) {
            var ar = [];
            for (var k in o) if (Object.prototype.hasOwnProperty.call(o, k)) ar[ar.length] = k;
            return ar;
        };
        return ownKeys(o);
    };
    return function (mod) {
        if (mod && mod.__esModule) return mod;
        var result = {};
        if (mod != null) for (var k = ownKeys(mod), i = 0; i < k.length; i++) if (k[i] !== "default") __createBinding(result, mod, k[i]);
        __setModuleDefault(result, mod);
        return result;
    };
})();
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const net = __importStar(require("net"));
const promise_1 = __importDefault(require("mysql2/promise"));
// Remote DB connection pool
const db = promise_1.default.createPool({
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
    socket.on('data', (data) => __awaiter(void 0, void 0, void 0, function* () {
        const message = data.toString('utf-8').trim();
        console.log(`[Client] ${message}`);
        const [command, username, password] = message.split('|');
        console.log("Before login");
        if (command.toLowerCase() === 'login') {
            try {
                const [rows] = yield db.query('SELECT * FROM users WHERE username = ? AND password = ?', [username, password]);
                if (rows.length > 0) {
                    // Optional: send all user data
                    const [allUsers] = yield db.query('SELECT * FROM users');
                    const userList = allUsers.map(user => {
                        return `${user.id},${user.username},${user.password},${user.email}`;
                    }).join('\n');
                    socket.write(`Login successful\n${userList}\n`);
                }
                else {
                    socket.write('Login failed\n');
                }
            }
            catch (err) {
                console.error('DB error:', err);
                socket.write('Internal server error\n');
            }
        }
        else if (command.toLowerCase() === 'signin') {
            try {
                yield db.query('INSERT INTO users (username, password) VALUES (?, ?)', [username, password]);
                socket.write('Signup successful\n');
            }
            catch (err) {
                console.error('DB error:', err);
                socket.write('Signup failed\n');
            }
        }
        else {
            socket.write(`Unknown command: ${command}\n`);
        }
    }));
    socket.on('end', () => console.log('C# client disconnected'));
    socket.on('error', (err) => console.error('Socket error:', err.message));
}).on('error', (err) => {
    console.log('Server error', err);
});
// Start listening
server.listen(9000, '127.0.0.1', () => {
    console.log('TCP server listening on 127.0.0.1:9000');
});
//# sourceMappingURL=server.js.map