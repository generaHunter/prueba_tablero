CREATE TABLE IF NOT EXISTS Tablero (
   IdTablero SERIAL PRIMARY KEY,
   Nombre VARCHAR(100) NOT NULL,
   Descripcion VARCHAR(200) NOT NULL,
   FechaCreacion Date NOT NULL,
   UserId Int NOT NULL,
  CONSTRAINT fk_usuario
  FOREIGN KEY(UserId)
  REFERENCES Usuario(UserId)
  ON DELETE CASCADE
);