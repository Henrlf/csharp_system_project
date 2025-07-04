﻿namespace Franco.Core.Enum;

public enum HttpCodeEnum
{
    // WARNING CODES
    DATA_NOT_FINDED = 101,

    // SUCCESS CODES 
    SUCCESS = 200,
    INSERT_DATA_OK = 201,
    UPDATE_DATA_OK = 202,
    DISABLE_DATA_OK = 203,
    DELETE_DATA_OK = 204,
    
    // ERROR CODES
    ERROR = 400,
    INVALID_DATA = 401,
    INSERT_DATA_ERROR = 402,
    UPDATE_DATA_ERROR = 403,
    DISABLE_DATA_ERROR = 404,
    DELETE_DATA_ERROR = 405,
    UNAUTHORIZE = 410,
    
}