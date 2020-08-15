namespace Advertise.Core.Helpers
{
    /// <remarks>
    ///     Database 	            GUID Column 	    SequentialGuidType
    ///     Microsoft SQL Server 	uniqueidentifier 	SequentialAtEnd
    ///     MySQL 	                char(36) 	        SequentialAsString
    ///     Oracle 	                raw(16) 	        SequentialAsBinary
    ///     PostgreSQL 	            uuid 	            SequentialAsString
    ///     SQLite  	            varies  	        varies
    /// </remarks>
    public enum SequentialGuidType
    {
        /// <summary>
        /// Use for MySQL char(36)
        /// Use for PostgreSQL uuid
        /// </summary>
        SequentialAsString,

        /// <summary>
        /// Use for Oracle raw(16)
        /// </summary>
        SequentialAsBinary,

        /// <summary>
        /// Use for Microsoft SQL Server uniqueidentifier
        /// </summary>
        SequentialAtEnd
    }
}