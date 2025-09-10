namespace ALOG.Enums;

public enum ECRUDAction
{
    //
    // Resumen:
    //     Ninguna
    None = 0,
    //
    // Resumen:
    //     Insertar
    Create = 1,
    //
    // Resumen:
    //     Actualizar
    Update = 2,
    //
    // Resumen:
    //     Eliminar
    Delete = 3,
    //
    // Resumen:
    //     Leer
    Read = 4
}

public enum TipoNodo
{
    None,
    SuperAdministrador,
    AdministradorEmpresa,
    UsuarioSistema
}

public enum TipoVista
{
    None,
    Principal,
    Parcial
}

public enum TipoMedio
{
    None,
    CorreoElectronico,
    Telefono,
    Celular,
    Fax
}

public enum TipoPosicionKeyButton
{
    Left,
    Right
}

public enum DimensPopup
{
    NoPopup,
    PopupSmall,
    PopupMedium,
    PopupLarge

}
