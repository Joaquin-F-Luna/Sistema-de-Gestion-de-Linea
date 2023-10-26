    SELECT 
    A.Id_Asignacion,
    A.idProyecto_FK,
    A.Area,
    A.estado_asignacion,
    A.Id_UsuarioAsignador,
    MA.IdMovimientos,
    MA.IdAsignaciones_FK,
    MA.IdUserAsignado_FK,
    MA.estadoMovimiento,
    MA.FechaMovimiento,
    MA.observacion
FROM 
    Asignaciones A
LEFT JOIN 
    movimientos_asignaciones MA ON A.Id_Asignacion = MA.IdAsignaciones_FK
    LEFT JOIN 
    Usuarios UAsignador ON A.Id_UsuarioAsignador = UAsignador.Id
LEFT JOIN 
    Usuarios UAsignado ON MA.IdUserAsignado_FK = UAsignado.Id

WHERE 
    A.idProyecto_FK = 1;

DELETE FROM Movimientos_Asignaciones;
DBCC CHECKIDENT ('Movimientos_Asignaciones', RESEED, 0);

DELETE FROM Asignaciones;
DBCC CHECKIDENT ('Asignaciones', RESEED, 0);

DELETE FROM historicoProyectos;
DBCC CHECKIDENT ('historicoProyectos', RESEED, 0);

DELETE FROM Proyecto;
DBCC CHECKIDENT ('Proyecto', RESEED, 0);





SELECT P.ID_Proyecto AS 'Proyecto', P.NumeroTicket AS 'Ticket', P.NumeroLinea AS 'Linea', MA.observacion AS 'Observaciones',
                                MA.estadoMovimiento AS 'Estado', MA.FechaMovimiento AS 'Fecha',UAsignado.Username AS 'Asignado a',
                                UAsignador.Username AS 'Creado por'
                                FROM Proyecto P
                                INNER JOIN Asignaciones A
                                                   ON P.ID_Proyecto = A.idProyecto_FK
                                LEFT JOIN 
                                      movimientos_asignaciones MA ON A.Id_Asignacion = MA.IdAsignaciones_FK
                                LEFT JOIN 
                                      Usuarios UAsignador ON A.Id_UsuarioAsignador = UAsignador.Id
                                LEFT JOIN 
                                     Usuarios UAsignado ON MA.IdUserAsignado_FK = UAsignado.Id

                                 WHERE P.ID_Proyecto = 1


