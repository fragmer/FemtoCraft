﻿// Part of FemtoCraft | Copyright 2012 Matvei Stefarov <me@matvei.org> | See LICENSE.txt
using System;
using System.IO;
using JetBrains.Annotations;

namespace FemtoCraft {
    static class Logger {
        const string LogFileName = "server.log";
        static readonly object LogLock = new object();


        [StringFormatMethod("message")]
        public static void LogError( [NotNull] string message, [NotNull] params object[] formatArgs ) {
            LogInternal( message, formatArgs, "ERROR ", true );
        }


        [StringFormatMethod( "message" )]
        public static void LogWarning( [NotNull] string message, [NotNull] params object[] formatArgs ) {
            LogInternal( message, formatArgs, "Warning ", true );
        }


        [StringFormatMethod( "message" )]
        public static void LogChat( [NotNull] string message, [NotNull] params object[] formatArgs ) {
            LogInternal( message, formatArgs, "Chat ", false );
        }


        [StringFormatMethod( "message" )]
        public static void Log( [NotNull] string message, [NotNull] params object[] formatArgs ) {
            LogInternal( message, formatArgs, "", false );
        }


        static void LogInternal( string message, object[] formatArgs, string prefix, bool error ) {
            lock( LogLock ) {
                string formattedMsg = String.Format( "{0} {1}> {2}",
                                                     Timestamp(),
                                                     prefix,
                                                     String.Format( message, formatArgs ) );
                if( error ) {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Error.WriteLine( formattedMsg );
                    Console.ResetColor();
                } else {
                    Console.WriteLine( formattedMsg );
                }
                File.AppendAllText( LogFileName, formattedMsg + Environment.NewLine );
            }
        }


        static string Timestamp() {
            return DateTime.Now.ToString( "yyyy'-'MM'-'dd' 'HH':'mm':'ss" );
        }
    }
}