﻿using System.Runtime.Serialization;

namespace Business.Exceptions;

public class AuthenticationException : Exception {
    public AuthenticationException() {
    }

    protected AuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context) {
    }

    public AuthenticationException(string? message) : base(message) {
    }

    public AuthenticationException(string? message, Exception? innerException) : base(message, innerException) {
    }
}