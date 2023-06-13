INSERT INTO AUTOR  (IdAutor, Descripcion , Estado, FechaCreacion)
VALUES
  (1, 'Gabriel García Márquez', 1, '2023-06-01 00:00:00'),
  (2, 'George Orwell', 1, '2023-06-01 00:00:00'),
  (3, 'Harper Lee', 1, '2023-06-01 00:00:00'),
  (4, 'Jane Austen', 1, '2023-06-01 00:00:00'),
  (5, 'Miguel de Cervantes', 1, '2023-06-01 00:00:00'),
  (6, 'William Shakespeare', 1, '2023-06-01 00:00:00'),
  (7, 'J.R.R. Tolkien', 1, '2023-06-01 00:00:00'),
  (8, 'Agatha Christie', 1, '2023-06-01 00:00:00'),
  (9, 'J.K. Rowling', 1, '2023-06-01 00:00:00'),
  (10, 'Ernest Hemingway', 1, '2023-06-01 00:00:00'),
  (11, 'Mark Twain', 1, '2023-06-01 00:00:00'),
  (12, 'Charles Dickens', 1, '2023-06-01 00:00:00'),
  (13, 'Virginia Woolf', 1, '2023-06-01 00:00:00'),
  (14, 'Fyodor Dostoevsky', 1, '2023-06-01 00:00:00'),
  (15, 'Leo Tolstoy', 1, '2023-06-01 00:00:00'),
  (16, 'Hermann Hesse', 1, '2023-06-01 00:00:00'),
  (17, 'Albert Camus', 1, '2023-06-01 00:00:00'),
  (18, 'Gabriela Mistral', 1, '2023-06-01 00:00:00'),
  (19, 'Octavio Paz', 1, '2023-06-01 00:00:00'),
  (20, 'Pablo Neruda', 1, '2023-06-01 00:00:00'),
  (21, 'Isabel Allende', 1, '2023-06-01 00:00:00'),
  (22, 'Mario Vargas Llosa', 1, '2023-06-01 00:00:00'),
  (23, 'Gabriel Mistral', 1, '2023-06-01 00:00:00'),
  (24, 'Rabindranath Tagore', 1, '2023-06-01 00:00:00'),
  (25, 'Jorge Luis Borges', 1, '2023-06-01 00:00:00'),
  (26, 'Emily Brontë', 1, '2023-06-01 00:00:00'),
  (27, 'Charlotte Brontë', 1, '2023-06-01 00:00:00'),
(28, 'Oscar Wilde', 1, '2023-06-01 00:00:00'),
(29, 'Alberto Moravia', 1, '2023-06-01 00:00:00'),
(30, 'H.P. Lovecraft', 1, '2023-06-01 00:00:00');

INSERT INTO CATEGORIA (IdCategoria, Descripcion, Estado, FechaCreacion)
VALUES
  (2, 'Ficción', 1, '2023-06-01 00:00:00'),
  (3, 'No ficción', 1, '2023-06-01 00:00:00'),
  (4, 'Misterio', 1, '2023-06-01 00:00:00'),
  (5, 'Romance', 1, '2023-06-01 00:00:00'),
  (6, 'Ciencia ficción', 1, '2023-06-01 00:00:00'),
  (7, 'Fantasía', 1, '2023-06-01 00:00:00'),
  (8, 'Historia', 1, '2023-06-01 00:00:00'),
  (9, 'Biografía', 1, '2023-06-01 00:00:00'),
  (10, 'Arte', 1, '2023-06-01 00:00:00'),
  (11, 'Autoayuda', 1, '2023-06-01 00:00:00'),
(12, 'Novela', 1, '2023-06-01 00:00:00'),
(13, 'Tecnologia', 1, '2023-06-01 00:00:00'),
  (14, 'Drama', 1, '2023-06-01 00:00:00');
  
 INSERT INTO TIPO_PERSONA  (IdTipoPersona, Descripcion, Estado, FechaCreacion)
VALUES
  (1, 'Administrador', 1, '2023-05-01 00:00:00'),
  (2, 'Empleado', 1, '2023-05-01 00:00:00'),
  (3, 'Lector', 1, '2023-05-01 00:00:00');
 
 INSERT INTO PERSONA (IdPersona, Nombre, Apellido, Correo, Clave, Codigo, idTipoPersona, Estado, FechaCreacion)
VALUES
  (1, 'Juan', 'Pérez', 'juan22@gmail.com', 'clave123', 'ABC123', 3, 1, '2023-06-01 00:00:00'),
  (2, 'María', 'González', 'mariago@gmail.com', 'password456', 'DEF456', 3, 1, '2023-06-01 00:00:00'),
  (3, 'Carlos', 'López', 'carlosmicky@hotmail.com', 'qwerty789', 'GHI789', 2, 1, '2023-06-01 00:00:00'),
  (4, 'Ana', 'Martínez', 'analana@outlook.com', 'securepassword', 'JKL012', 3, 1, '2023-06-01 00:00:00'),
  (5, 'Luis', 'Rodríguez', 'luismiguel@gmail.com', 'password123', 'MNO345', 2, 1, '2023-06-01 00:00:00'),
  (6, 'Admin', 'Pro', 'admin@gmail.com', 'admin123', 'MNO145', 1, 1, '2023-06-01 00:00:00');
 
 
 
INSERT INTO LIBRO  (IdLibro, Titulo, RutaPortada, NombrePortada, idAutor, idCategoria, idEditorial, Ubicacion, Ejemplares, Estado, FechaCreacion)
VALUES
  (1, 'Cien años de soledad', 'images/', '1.jpg', 1, 2, 1, 'Estantería 1', 10, 1, '2023-06-01 00:00:00'),
  (2, 'El amor en los tiempos del cólera', 'images/', '2.jpg', 1, 2, 1, 'Estantería 2', 8, 1, '2023-06-01 00:00:00'),
  (3, 'Crónica de una muerte anunciada', 'images/', '3.jpg', 1, 2, 2, 'Estantería 3', 6, 1, '2023-06-01 00:00:00'),
  (4, '1984', 'images/', '4.jpg', 2, 6, 1, 'Estantería 1', 10, 1, '2023-06-01 00:00:00'),
  (5, 'Rebelión en la granja', 'images/', '5.jpg', 2, 7, 3, 'Estantería 1', 8, 1, '2023-06-01 00:00:00'),
  (6, 'Harry Potter y la piedra filosofal', 'images/', '6.jpg', 9, 12, 5, 'Estantería 4', 10, 1, '2023-06-01 00:00:00'),
  (7, 'El viejo y el mar', 'images/', '7.jpg', 10, 12, 4, 'Estantería 4', 7, 1, '2023-06-01 00:00:00'),
  (8, 'Hamlet', 'images/', '8.jpg', 6, 13, 5, 'Estantería 19', 8, 1, '2023-06-01 00:00:00');
  (9, 'Orgullo y prejuicio', 'images/', '9.jpg', 4, 5, 4, 'Estantería 3', 10, 1, '2023-06-01 00:00:00'),
  (10, 'El gran Gatsby', 'images/', '10.jpg', 6, 5, 1, 'Estantería 3', 6, 1, '2023-06-01 00:00:00'),
  (11, 'Jane Eyre', 'images/', '11.jpg', 7, 12, 1, 'Estantería 4', 27, 1, '2023-06-01 00:00:00'),
 
 INSERT INTO EDITORIAL  (IdEditorial, Descripcion, Estado, FechaCreacion)
VALUES
  (1, 'Penguin Random House', 1, '2023-06-01 00:00:00'),
  (2, 'HarperCollins Publishers', 1, '2023-06-01 00:00:00'),
  (3, 'Hachette Livre', 1, '2023-06-01 00:00:00'),
  (4, 'Simon & Schuster', 1, '2023-06-01 00:00:00'),
  (5, 'Macmillan Publishers', 1, '2023-06-01 00:00:00');
 
 INSERT INTO ESTADO_PRESTAMO(IdEstadoPrestamo,Descripcion) VALUES
(1,'Pendiente'),
(2,'Devuelto')