// Provisional 256x256, 4-colour round woven-mat icon master.
// No copied third-party pixels or AI output. Needs author art acceptance.
// Usage: node Art/Sources/EnzaTexture.js
const fs = require("fs");
const path = require("path");
const bytes = [];
function u32(v) { return [(v >>> 24) & 255, (v >>> 16) & 255, (v >>> 8) & 255, v & 255]; }
function chunk(name, data) {
  const nameBytes = [...name].map(x => x.charCodeAt(0));
  let c = 0xffffffff;
  for (const b of [...nameBytes, ...data]) {
    c ^= b;
    for (let i = 0; i < 8; i++) c = (c & 1) ? ((c >>> 1) ^ 0xedb88320) : (c >>> 1);
  }
  bytes.push(...u32(data.length), ...nameBytes, ...data, ...u32((c ^ 0xffffffff) >>> 0));
}
bytes.push(137, 80, 78, 71, 13, 10, 26, 10);
chunk("IHDR", [...u32(256), ...u32(256), 2, 3, 0, 0, 0]);
chunk("PLTE", [0, 0, 0, 76, 57, 41, 157, 125, 71, 201, 164, 109]);
chunk("tRNS", [0, 255, 255, 255]);
const raw = [];
for (let y = 0; y < 256; y++) {
  raw.push(0);
  for (let x = 0; x < 256; x += 4) {
    let packed = 0;
    for (let dx = 0; dx < 4; dx++) {
      const xx = x + dx - 128, yy = y - 132;
      const r = Math.sqrt(xx * xx + yy * yy);
      let v = 0;
      if (r <= 100) {
        const edge = r > 94;
        const arc = (Math.atan2(yy, xx) + Math.PI * 2) % (Math.PI * 2 / 36);
        v = edge ? 1 : ((Math.floor(r / 8) % 2) ? 2 : 3);
        if (r > 10 && r < 94 && (arc < .018 || r % 13 < 2)) v = 1;
        if (r <= 10) v = r > 6 ? 1 : 2;
      }
      packed |= v << (6 - 2 * dx);
    }
    raw.push(packed);
  }
}
let a = 1, b = 0;
for (const v of raw) { a = (a + v) % 65521; b = (b + a) % 65521; }
const z = [0x78, 0x01];
for (let off = 0; off < raw.length;) {
  const size = Math.min(65535, raw.length - off), last = off + size === raw.length;
  z.push(last ? 1 : 0, size & 255, (size >>> 8) & 255,
    (~size) & 255, ((~size) >>> 8) & 255, ...raw.slice(off, off + size));
  off += size;
}
z.push(...u32((((b << 16) >>> 0) | a) >>> 0));
chunk("IDAT", z);
chunk("IEND", []);
const output = path.join(__dirname, "../../Textures/Things/Building/Furniture/Enza.png");
fs.mkdirSync(path.dirname(output), { recursive: true });
fs.writeFileSync(output, Buffer.from(bytes));
