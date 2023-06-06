//Categoria
CREATE PROCEDURE `DB_BIBLIOTECA`.`sp_RegistrarCategoria`(
    IN pDescripcion VARCHAR(50),
    OUT pResultado BIT
)
BEGIN
    SET pResultado = 1;

    IF NOT EXISTS (SELECT * FROM CATEGORIA WHERE Descripcion = pDescripcion) THEN
        INSERT INTO CATEGORIA (Descripcion) VALUES (pDescripcion);
    ELSE
        SET pResultado = 0;
    END IF;
END



//Modificar
CREATE PROCEDURE sp_ModificarCategoria(
    IN pIdCategoria INT,
    IN pDescripcion VARCHAR(60),
    IN pEstado BIT,
    OUT pResultado BIT
)
BEGIN
    SET pResultado = 1;
    
    IF NOT EXISTS (SELECT * FROM CATEGORIA WHERE Descripcion = pDescripcion AND IdCategoria != pIdCategoria) THEN
        UPDATE CATEGORIA SET
            Descripcion = pDescripcion,
            Estado = pEstado
        WHERE IdCategoria = pIdCategoria;
    ELSE
        SET pResultado = 0;
    END IF;
END



//Autor
CREATE PROCEDURE sp_RegistrarAutor(
    IN pDescripcion VARCHAR(50),
    OUT pResultado BIT
)
BEGIN
    SET pResultado = 1;

    IF NOT EXISTS (SELECT * FROM AUTOR  WHERE Descripcion = pDescripcion) THEN
        INSERT INTO AUTOR (Descripcion) VALUES (pDescripcion);
    ELSE
        SET pResultado = 0;
    END IF;
END

//Modificar
CREATE PROCEDURE sp_ModificarAutor(
    IN pIdAutor INT,
    IN pDescripcion VARCHAR(60),
    IN pEstado BIT,
    OUT pResultado BIT
)
BEGIN
    SET pResultado = 1;
    
    IF NOT EXISTS (SELECT * FROM AUTOR WHERE Descripcion = pDescripcion AND IdAutor != pIdAutor) THEN
        UPDATE AUTOR SET
            Descripcion = pDescripcion,
            Estado = pEstado
        WHERE IdAutor = pIdAutor;
    ELSE
        SET pResultado = 0;
    END IF;
END

//EDITORIAL
CREATE PROCEDURE sp_RegistrarEditorial(
    IN pDescripcion VARCHAR(50),
    OUT pResultado BIT
)
BEGIN
    SET pResultado = 1;

    IF NOT EXISTS (SELECT * FROM EDITORIAL WHERE Descripcion = pDescripcion) THEN
        INSERT INTO EDITORIAL (Descripcion) VALUES (pDescripcion);
    ELSE
        SET pResultado = 0;
    END IF;
END

//EDITORIAL
CREATE PROCEDURE sp_ModificarEditorial(
    IN pIdEditorial INT,
    IN pDescripcion VARCHAR(60),
    IN pEstado BIT,
    OUT pResultado BIT
)
BEGIN
    SET pResultado = 1;
    
    IF NOT EXISTS (SELECT * FROM EDITORIAL WHERE Descripcion = pDescripcion AND IdEditorial != pIdEditorial) THEN
        UPDATE EDITORIAL SET
            Descripcion = pDescripcion,
            Estado = pEstado
        WHERE IdEditorial = pIdEditorial;
    ELSE
        SET pResultado = 0;
    END IF;
END


//Libro
CREATE PROCEDURE `DB_BIBLIOTECA`.`sp_registrarLibro`(
    IN pTitulo VARCHAR(100),
    IN pRutaPortada VARCHAR(100),
    IN pNombrePortada VARCHAR(100),
    IN pIdAutor INT,
    IN pIdCategoria INT,
    IN pIdEditorial INT,
    IN pUbicacion VARCHAR(100),
    IN pEjemplares INT,
    OUT pResultado INT
)
BEGIN
    SET pResultado = 0;
    
    IF NOT EXISTS (SELECT * FROM LIBRO WHERE Titulo = pTitulo) THEN
        INSERT INTO LIBRO(Titulo, RutaPortada, NombrePortada, IdAutor, IdCategoria, IdEditorial, Ubicacion, Ejemplares)
        VALUES (pTitulo, pRutaPortada, pNombrePortada, pIdAutor, pIdCategoria, pIdEditorial, pUbicacion, pEjemplares);
        
        SET pResultado = LAST_INSERT_ID();
    END IF;
END


//Libro
CREATE PROCEDURE sp_modificarLibro(
    IN p_IdLibro INT,
    IN p_Titulo VARCHAR(100),
    IN p_IdAutor INT,
    IN p_IdCategoria INT,
    IN p_IdEditorial INT,
    IN p_Ubicacion VARCHAR(100),
    IN p_Ejemplares INT,
    IN p_Estado BIT,
    OUT p_Resultado BIT
)
BEGIN
    SET p_Resultado = 0;

    IF NOT EXISTS (SELECT * FROM LIBRO WHERE Titulo = p_Titulo AND IdLibro != p_IdLibro) THEN
        UPDATE LIBRO SET 
            Titulo = p_Titulo,
            IdAutor = p_IdAutor,
            IdCategoria = p_IdCategoria,
            IdEditorial = p_IdEditorial,
            Ubicacion = p_Ubicacion,
            Ejemplares = p_Ejemplares,
            Estado = p_Estado
        WHERE IdLibro = p_IdLibro;

        SET p_Resultado = 1;
    END IF;
END




//Persona
CREATE PROCEDURE sp_ModificarPersona(
    IN pIdPersona INT,
    IN pNombre VARCHAR(50),
    IN pApellido VARCHAR(50),
    IN pCorreo VARCHAR(50),
    IN pClave VARCHAR(50),
    IN pIdTipoPersona INT,
    IN pEstado BIT,
    OUT pResultado BIT
)
BEGIN
    SET pResultado = 1;

    IF NOT EXISTS (SELECT * FROM PERSONA WHERE Correo = pCorreo AND IdPersona != pIdPersona) THEN
        UPDATE PERSONA SET 
        Nombre = pNombre,
        Apellido = pApellido,
        Correo = pCorreo,
        Clave = pClave,
        IdTipoPersona = pIdTipoPersona,
        Estado = pEstado
        WHERE IdPersona = pIdPersona;
    ELSE
        SET pResultado = 0;
    END IF;

END;

//Persona
CREATE PROCEDURE sp_RegistrarPersona(
    IN pNombre VARCHAR(50),
    IN pApellido VARCHAR(50),
    IN pCorreo VARCHAR(50),
    IN pClave VARCHAR(50),
    IN pIdTipoPersona INT,
    OUT pResultado BIT
)
BEGIN
    DECLARE IDPERSONA INT;

    SET pResultado = 1;

    IF NOT EXISTS (SELECT * FROM PERSONA WHERE correo = pCorreo) THEN
        INSERT INTO PERSONA(Nombre, Apellido, Correo, Clave, IdTipoPersona) 
        VALUES (pNombre, pApellido, pCorreo, pClave, pIdTipoPersona);

        SET IDPERSONA = LAST_INSERT_ID();

        IF (pIdTipoPersona = 3) THEN
            UPDATE PERSONA SET 
            Codigo = fn_obtenercorrelativo(IDPERSONA),
            Clave = fn_obtenercorrelativo(IDPERSONA)
            WHERE IdPersona = IDPERSONA;
        END IF;
    ELSE
        SET pResultado = 0;
    END IF;

END;


//Prestamo
CREATE PROCEDURE sp_RegistrarPrestamo(
    IN pIdEstadoPrestamo INT,
    IN pIdPersona INT,
    IN pIdLibro INT,
    IN pFechaDevolucion DATETIME,
    IN pEstadoEntregado VARCHAR(500),
    OUT pResultado BIT
)
BEGIN
    SET pResultado = 1;

    INSERT INTO PRESTAMO (IdEstadoPrestamo, IdPersona, IdLibro, FechaDevolucion, EstadoEntregado)
    VALUES (pIdEstadoPrestamo, pIdPersona, pIdLibro, pFechaDevolucion, pEstadoEntregado);

END;


//PRESTAMO
CREATE PROCEDURE sp_existePrestamo(
    IN pIdPersona INT,
    IN pIdLibro INT,
    OUT pResultado BIT
)
BEGIN
    SET pResultado = 0;

    IF EXISTS (SELECT * FROM PRESTAMO WHERE IdPersona = pIdPersona AND IdLibro = pIdLibro AND IdEstadoPrestamo <> 2) THEN
        SET pResultado = 1;
    END IF;
    
END

//funcion
SET @@log_bin_trust_function_creators = 1;

CREATE FUNCTION fn_obtenercorrelativo(numero INT)
RETURNS VARCHAR(100)
DETERMINISTIC
BEGIN
    DECLARE correlativo VARCHAR(100);
    SET correlativo = CONCAT('LE', LPAD(numero, 6, '0'));
    RETURN correlativo;
END;

//iMAGEN
CREATE PROCEDURE sp_actualizarRutaImagen(
    IN p_IdLibro INT,
    IN p_NombrePortada VARCHAR(500)
)
BEGIN
    UPDATE libro SET NombrePortada = p_NombrePortada WHERE IdLibro = p_IdLibro;
END