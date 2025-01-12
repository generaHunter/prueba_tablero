CREATE TABLE IF NOT EXISTS Tarea (
   IdTarea SERIAL PRIMARY KEY,
   Titulo VARCHAR(100) NOT NULL,
   Descripcion VARCHAR(200) NOT NULL,
   FechaCreacion Date NOT NULL,
   IdEstado Int,
   IdTablero Int NOT NULL,
   UserId Int NOT NULL,
  CONSTRAINT fk_usuario
  FOREIGN KEY(UserId)
  REFERENCES Usuario(UserId)
  ON DELETE CASCADE,
   CONSTRAINT fk_estado
      FOREIGN KEY(IdEstado)
	  REFERENCES Estado(IdEstado)
	  ON DELETE SET NULL,
    CONSTRAINT fk_tablero
	  FOREIGN KEY(IdTablero)
	  REFERENCES Tablero(IdTablero)
	  ON DELETE CASCADE
);