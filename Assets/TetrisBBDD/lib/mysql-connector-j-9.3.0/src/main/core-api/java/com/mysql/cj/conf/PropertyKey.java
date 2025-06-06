/*
 * Copyright (c) 2018, 2025, Oracle and/or its affiliates.
 *
 * This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License, version 2.0, as published by
 * the Free Software Foundation.
 *
 * This program is designed to work with certain software that is licensed under separate terms, as designated in a particular file or component or in
 * included license documentation. The authors of MySQL hereby grant you an additional permission to link the program and your derivative works with the
 * separately licensed software that they have either included with the program or referenced in the documentation.
 *
 * Without limiting anything contained in the foregoing, this file, which is part of MySQL Connector/J, is also subject to the Universal FOSS Exception,
 * version 1.0, a copy of which can be found at http://oss.oracle.com/licenses/universal-foss-exception.
 *
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License, version 2.0, for more details.
 *
 * You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc.,
 * 51 Franklin St, Fifth Floor, Boston, MA 02110-1301 USA
 */

package com.mysql.cj.conf;

import java.util.Map;
import java.util.TreeMap;

/**
 * PropertyKey handles connection property names, their camel-case aliases and case sensitivity.
 */
public enum PropertyKey {

    /*
     * Properties individually managed after parsing connection string. These property keys are case insensitive.
     */
    /** The database user name. */
    USER("user", false),
    /** The database user password. */
    PASSWORD("password", false),
    /** The hostname value from the properties instance passed to the driver. */
    HOST("host", false),
    /** The port number value from the properties instance passed to the driver. */
    PORT("port", false),
    /** The communications protocol. Possible values: "tcp" and "pipe". */
    PROTOCOL("protocol", false),
    /** The name pipes path to use when "protocol=pipe'. */
    PATH("path", "namedPipePath", false),
    /** The server type in a replication setup. Possible values: "source" and "replica". */
    TYPE("type", false),
    /** The address value ("host:port") from the properties instance passed to the driver. */
    ADDRESS("address", false),
    /** The host priority in a list of hosts. */
    PRIORITY("priority", false),
    /** The database value from the properties instance passed to the driver. */
    DBNAME("dbname", false), //

    allowLoadLocalInfile("allowLoadLocalInfile", true), //
    allowLoadLocalInfileInPath("allowLoadLocalInfileInPath", true), //
    allowMultiQueries("allowMultiQueries", true), //
    allowNanAndInf("allowNanAndInf", true), //
    allowPublicKeyRetrieval("allowPublicKeyRetrieval", true), //
    allowReplicaDownConnections("allowReplicaDownConnections", true), //
    allowSourceDownConnections("allowSourceDownConnections", true), //
    allowUrlInLocalInfile("allowUrlInLocalInfile", true), //
    alwaysSendSetIsolation("alwaysSendSetIsolation", true), //
    authenticationOpenidConnectCallbackHandler("authenticationOpenidConnectCallbackHandler", true), //
    authenticationPlugins("authenticationPlugins", true), //
    authenticationWebAuthnCallbackHandler("authenticationWebAuthnCallbackHandler", true), //
    autoClosePStmtStreams("autoClosePStmtStreams", true), //
    autoGenerateTestcaseScript("autoGenerateTestcaseScript", true), //
    autoReconnect("autoReconnect", true), //
    autoReconnectForPools("autoReconnectForPools", true), //
    autoSlowLog("autoSlowLog", true), //
    blobsAreStrings("blobsAreStrings", true), //
    blobSendChunkSize("blobSendChunkSize", true), //
    cacheCallableStmts("cacheCallableStmts", true), //
    cacheDefaultTimeZone("cacheDefaultTimeZone", "cacheDefaultTimezone", true), //
    cachePrepStmts("cachePrepStmts", true), //
    cacheResultSetMetadata("cacheResultSetMetadata", true), //
    cacheServerConfiguration("cacheServerConfiguration", true), //
    callableStmtCacheSize("callableStmtCacheSize", true), //
    characterEncoding("characterEncoding", true), //
    characterSetResults("characterSetResults", true), //
    clientCertificateKeyStorePassword("clientCertificateKeyStorePassword", true), //
    clientCertificateKeyStoreType("clientCertificateKeyStoreType", true), //
    clientCertificateKeyStoreUrl("clientCertificateKeyStoreUrl", true), //
    clientInfoProvider("clientInfoProvider", true), //
    clobberStreamingResults("clobberStreamingResults", true), //
    clobCharacterEncoding("clobCharacterEncoding", true), //
    compensateOnDuplicateKeyUpdateCounts("compensateOnDuplicateKeyUpdateCounts", true), //
    connectionAttributes("connectionAttributes", true), //
    connectionCollation("connectionCollation", true), //
    connectionLifecycleInterceptors("connectionLifecycleInterceptors", true), //
    connectionTimeZone("connectionTimeZone", "serverTimezone", true), //
    connectTimeout("connectTimeout", true), //
    continueBatchOnError("continueBatchOnError", true), //
    createDatabaseIfNotExist("createDatabaseIfNotExist", true), //
    customCharsetMapping("customCharsetMapping", true), //
    databaseTerm("databaseTerm", true), //
    defaultAuthenticationPlugin("defaultAuthenticationPlugin", true), //
    defaultFetchSize("defaultFetchSize", true), //
    detectCustomCollations("detectCustomCollations", true), //
    disabledAuthenticationPlugins("disabledAuthenticationPlugins", true), //
    disconnectOnExpiredPasswords("disconnectOnExpiredPasswords", true), //
    dnsSrv("dnsSrv", true), //
    dontCheckOnDuplicateKeyUpdateInSQL("dontCheckOnDuplicateKeyUpdateInSQL", true), //
    dontTrackOpenResources("dontTrackOpenResources", true), //
    dumpQueriesOnException("dumpQueriesOnException", true), //
    elideSetAutoCommits("elideSetAutoCommits", true), //
    emptyStringsConvertToZero("emptyStringsConvertToZero", true), //
    emulateLocators("emulateLocators", true), //
    emulateUnsupportedPstmts("emulateUnsupportedPstmts", true), //
    enableEscapeProcessing("enableEscapeProcessing", true), //
    enablePacketDebug("enablePacketDebug", true), //
    enableQueryTimeouts("enableQueryTimeouts", true), //
    exceptionInterceptors("exceptionInterceptors", true), //
    explainSlowQueries("explainSlowQueries", true), //
    failOverReadOnly("failOverReadOnly", true), //
    fallbackToSystemKeyStore("fallbackToSystemKeyStore", true), //
    fallbackToSystemTrustStore("fallbackToSystemTrustStore", true), //
    fipsCompliantJsse("fipsCompliantJsse", true), //
    forceConnectionTimeZoneToSession("forceConnectionTimeZoneToSession", true), //
    functionsNeverReturnBlobs("functionsNeverReturnBlobs", true), //
    gatherPerfMetrics("gatherPerfMetrics", true), //
    generateSimpleParameterMetadata("generateSimpleParameterMetadata", true), //
    getProceduresReturnsFunctions("getProceduresReturnsFunctions", true), //
    ha_enableJMX("ha.enableJMX", "haEnableJMX", true), //
    ha_loadBalanceStrategy("ha.loadBalanceStrategy", "haLoadBalanceStrategy", true), //
    holdResultsOpenOverStatementClose("holdResultsOpenOverStatementClose", true), //
    idTokenFile("idTokenFile", true), //
    ignoreNonTxTables("ignoreNonTxTables", true), //
    includeInnodbStatusInDeadlockExceptions("includeInnodbStatusInDeadlockExceptions", true), //
    includeThreadDumpInDeadlockExceptions("includeThreadDumpInDeadlockExceptions", true), //
    includeThreadNamesAsStatementComment("includeThreadNamesAsStatementComment", true), //
    initialTimeout("initialTimeout", true), //
    interactiveClient("interactiveClient", true), //
    jdbcCompliantTruncation("jdbcCompliantTruncation", true), //
    keyManagerFactoryProvider("KeyManagerFactoryProvider", true), //
    keyStoreProvider("keyStoreProvider", true), //
    largeRowSizeThreshold("largeRowSizeThreshold", true), //
    ldapServerHostname("ldapServerHostname", true), //
    loadBalanceAutoCommitStatementRegex("loadBalanceAutoCommitStatementRegex", true), //
    loadBalanceAutoCommitStatementThreshold("loadBalanceAutoCommitStatementThreshold", true), //
    loadBalanceBlocklistTimeout("loadBalanceBlocklistTimeout", true), //
    loadBalanceConnectionGroup("loadBalanceConnectionGroup", true), //
    loadBalanceExceptionChecker("loadBalanceExceptionChecker", true), //
    loadBalanceHostRemovalGracePeriod("loadBalanceHostRemovalGracePeriod", true), //
    loadBalancePingTimeout("loadBalancePingTimeout", true), //
    loadBalanceSQLExceptionSubclassFailover("loadBalanceSQLExceptionSubclassFailover", true), //
    loadBalanceSQLStateFailover("loadBalanceSQLStateFailover", true), //
    loadBalanceValidateConnectionOnSwapServer("loadBalanceValidateConnectionOnSwapServer", true), //
    localSocketAddress("localSocketAddress", true), //
    locatorFetchBufferSize("locatorFetchBufferSize", true), //
    logger("logger", true), //
    logSlowQueries("logSlowQueries", true), //
    logXaCommands("logXaCommands", true), //
    maintainTimeStats("maintainTimeStats", true), //
    maxAllowedPacket("maxAllowedPacket", true), //
    maxByteArrayAsHex("maxByteArrayAsHex", true), //
    maxQuerySizeToLog("maxQuerySizeToLog", true), //
    maxReconnects("maxReconnects", true), //
    maxRows("maxRows", true), //
    metadataCacheSize("metadataCacheSize", true), //
    netTimeoutForStreamingResults("netTimeoutForStreamingResults", true), //
    noAccessToProcedureBodies("noAccessToProcedureBodies", true), //
    noDatetimeStringSync("noDatetimeStringSync", true), //
    nullDatabaseMeansCurrent("nullDatabaseMeansCurrent", "nullCatalogMeansCurrent", true), //
    ociConfigFile("ociConfigFile", true), //
    ociConfigProfile("ociConfigProfile", true), //
    openTelemetry("openTelemetry", true), //
    overrideSupportsIntegrityEnhancementFacility("overrideSupportsIntegrityEnhancementFacility", true), //
    packetDebugBufferSize("packetDebugBufferSize", true), //
    padCharsWithSpace("padCharsWithSpace", true), //
    paranoid("paranoid", false), //
    password1("password1", true), //
    password2("password2", true), //
    password3("password3", true), //
    passwordCharacterEncoding("passwordCharacterEncoding", true), //
    pedantic("pedantic", true), //
    pinGlobalTxToPhysicalConnection("pinGlobalTxToPhysicalConnection", true), //
    populateInsertRowWithDefaultValues("populateInsertRowWithDefaultValues", true), //
    prepStmtCacheSize("prepStmtCacheSize", true), //
    prepStmtCacheSqlLimit("prepStmtCacheSqlLimit", true), //
    preserveInstants("preserveInstants", true), //
    processEscapeCodesForPrepStmts("processEscapeCodesForPrepStmts", true), //
    profilerEventHandler("profilerEventHandler", true), //
    profileSQL("profileSQL", true), //
    propertiesTransform("propertiesTransform", true), //
    queriesBeforeRetrySource("queriesBeforeRetrySource", true), //
    queryInfoCacheFactory("queryInfoCacheFactory", "parseInfoCacheFactory", true), //
    queryInterceptors("queryInterceptors", true), //
    queryTimeoutKillsConnection("queryTimeoutKillsConnection", true), //
    readFromSourceWhenNoReplicas("readFromSourceWhenNoReplicas", true), //
    readOnlyPropagatesToServer("readOnlyPropagatesToServer", true), //
    reconnectAtTxEnd("reconnectAtTxEnd", true), //
    replicationConnectionGroup("replicationConnectionGroup", true), //
    reportMetricsIntervalMillis("reportMetricsIntervalMillis", true), //
    requireSSL("requireSSL", true), //
    resourceId("resourceId", true), //
    resultSetSizeThreshold("resultSetSizeThreshold", true), //
    retriesAllDown("retriesAllDown", true), //
    rewriteBatchedStatements("rewriteBatchedStatements", true), //
    rollbackOnPooledClose("rollbackOnPooledClose", true), //
    scrollTolerantForwardOnly("scrollTolerantForwardOnly", true), //
    secondsBeforeRetrySource("secondsBeforeRetrySource", true), //
    selfDestructOnPingMaxOperations("selfDestructOnPingMaxOperations", true), //
    selfDestructOnPingSecondsLifetime("selfDestructOnPingSecondsLifetime", true), //
    sendFractionalSeconds("sendFractionalSeconds", true), //
    sendFractionalSecondsForTime("sendFractionalSecondsForTime", true), //
    serverAffinityOrder("serverAffinityOrder", true), //
    serverConfigCacheFactory("serverConfigCacheFactory", true), //
    serverRSAPublicKeyFile("serverRSAPublicKeyFile", true), //
    sessionVariables("sessionVariables", true), //
    slowQueryThresholdMillis("slowQueryThresholdMillis", true), //
    slowQueryThresholdNanos("slowQueryThresholdNanos", true), //
    socketFactory("socketFactory", true), //
    socketTimeout("socketTimeout", true), //
    socksProxyHost("socksProxyHost", true), //
    socksProxyPort("socksProxyPort", true), //
    socksProxyRemoteDns("socksProxyRemoteDns", true), //
    sslContextProvider("sslContextProvider", true), //
    sslMode("sslMode", true), //
    strictUpdates("strictUpdates", true), //
    tcpKeepAlive("tcpKeepAlive", true), //
    tcpNoDelay("tcpNoDelay", true), //
    tcpRcvBuf("tcpRcvBuf", true), //
    tcpSndBuf("tcpSndBuf", true), //
    tcpTrafficClass("tcpTrafficClass", true), //
    tinyInt1isBit("tinyInt1isBit", true), //
    tlsCiphersuites("tlsCiphersuites", "enabledSSLCipherSuites", true), //
    tlsVersions("tlsVersions", "enabledTLSProtocols", true), //
    traceProtocol("traceProtocol", true), //
    trackSessionState("trackSessionState", true), //
    transformedBitIsBoolean("transformedBitIsBoolean", true), //
    treatMysqlDatetimeAsTimestamp("treatMysqlDatetimeAsTimestamp", true), //
    treatUtilDateAsTimestamp("treatUtilDateAsTimestamp", true), //
    trustCertificateKeyStorePassword("trustCertificateKeyStorePassword", true), //
    trustCertificateKeyStoreType("trustCertificateKeyStoreType", true), //
    trustCertificateKeyStoreUrl("trustCertificateKeyStoreUrl", true), //
    trustManagerFactoryProvider("trustManagerFactoryProvider", true), //
    ultraDevHack("ultraDevHack", true), //
    useAffectedRows("useAffectedRows", true), //
    useColumnNamesInFindColumn("useColumnNamesInFindColumn", true), //
    useCompression("useCompression", true), //
    useConfigs("useConfigs", true), //
    useCursorFetch("useCursorFetch", true), //
    useHostsInPrivileges("useHostsInPrivileges", true), //
    useInformationSchema("useInformationSchema", true), //
    useLocalSessionState("useLocalSessionState", true), //
    useLocalTransactionState("useLocalTransactionState", true), //
    useNanosForElapsedTime("useNanosForElapsedTime", true), //
    useOldAliasMetadataBehavior("useOldAliasMetadataBehavior", true), //
    useOnlyServerErrorMessages("useOnlyServerErrorMessages", true), //
    useReadAheadInput("useReadAheadInput", true), //
    useServerPrepStmts("useServerPrepStmts", true), //
    useSSL("useSSL", true), //
    useStreamLengthsInPrepStmts("useStreamLengthsInPrepStmts", true), //
    useUnbufferedInput("useUnbufferedInput", true), //
    useUsageAdvisor("useUsageAdvisor", true), //
    verifyServerCertificate("verifyServerCertificate", true), //

    xdevapiAsyncResponseTimeout("xdevapi.asyncResponseTimeout", "xdevapiAsyncResponseTimeout", true), //
    xdevapiAuth("xdevapi.auth", "xdevapiAuth", true), //
    xdevapiConnectTimeout("xdevapi.connect-timeout", "xdevapiConnectTimeout", true), //
    xdevapiConnectionAttributes("xdevapi.connection-attributes", "xdevapiConnectionAttributes", true), //
    xdevapiCompression("xdevapi.compression", "xdevapiCompression", true), //
    xdevapiCompressionAlgorithms("xdevapi.compression-algorithms", "xdevapiCompressionAlgorithms", true), //
    xdevapiCompressionExtensions("xdevapi.compression-extensions", "xdevapiCompressionExtensions", true), //
    xdevapiDnsSrv("xdevapi.dns-srv", "xdevapiDnsSrv", true), //
    xdevapiFallbackToSystemKeyStore("xdevapi.fallback-to-system-keystore", "xdevapiFallbackToSystemKeyStore", true), //
    xdevapiFallbackToSystemTrustStore("xdevapi.fallback-to-system-truststore", "xdevapiFallbackToSystemTrustStore", true), //
    xdevapiSslKeyStorePassword("xdevapi.ssl-keystore-password", "xdevapiSslKeystorePassword", true), //
    xdevapiSslKeyStoreType("xdevapi.ssl-keystore-type", "xdevapiSslKeystoreType", true), //
    xdevapiSslKeyStoreUrl("xdevapi.ssl-keystore", "xdevapiSslKeystore", true), //
    xdevapiSslMode("xdevapi.ssl-mode", "xdevapiSslMode", true), //
    xdevapiSslTrustStorePassword("xdevapi.ssl-truststore-password", "xdevapiSslTruststorePassword", true), //
    xdevapiSslTrustStoreType("xdevapi.ssl-truststore-type", "xdevapiSslTruststoreType", true), //
    xdevapiSslTrustStoreUrl("xdevapi.ssl-truststore", "xdevapiSslTruststore", true), //
    xdevapiTlsCiphersuites("xdevapi.tls-ciphersuites", "xdevapiTlsCiphersuites", true), //
    xdevapiTlsVersions("xdevapi.tls-versions", "xdevapiTlsVersions", true), //

    yearIsDateType("yearIsDateType", true), //
    zeroDateTimeBehavior("zeroDateTimeBehavior", true) //
    ;

    private String keyName;
    private String ccAlias = null;
    private boolean isCaseSensitive = false;

    private static Map<String, PropertyKey> caseInsensitiveValues = new TreeMap<>(String.CASE_INSENSITIVE_ORDER);
    static {
        for (PropertyKey pk : values()) {
            if (!pk.isCaseSensitive) {
                caseInsensitiveValues.put(pk.getKeyName(), pk);
                if (pk.getCcAlias() != null) {
                    caseInsensitiveValues.put(pk.getCcAlias(), pk);
                }
            }
        }
    }

    /**
     * Initializes each enum element with the proper key name to be used in the connection string or properties maps.
     *
     * @param keyName
     *            the key name for the enum element.
     * @param isCaseSensitive
     *            is this name case sensitive
     */
    PropertyKey(String keyName, boolean isCaseSensitive) {
        this.keyName = keyName;
        this.isCaseSensitive = isCaseSensitive;
    }

    /**
     * Initializes each enum element with the proper key name to be used in the connection string or properties maps.
     *
     * @param keyName
     *            the key name for the enum element.
     * @param alias
     *            camel-case alias key name
     * @param isCaseSensitive
     *            is this name case sensitive
     */
    PropertyKey(String keyName, String alias, boolean isCaseSensitive) {
        this(keyName, isCaseSensitive);
        this.ccAlias = alias;
    }

    @Override
    public String toString() {
        return this.keyName;
    }

    /**
     * Gets the key name of this enum element.
     *
     * @return
     *         the key name associated with the enum element.
     */
    public String getKeyName() {
        return this.keyName;
    }

    /**
     * Gets the camel-case alias key name of this enum element.
     *
     * @return
     *         the camel-case alias key name associated with the enum element or null.
     */
    public String getCcAlias() {
        return this.ccAlias;
    }

    /**
     * Looks for a {@link PropertyKey} that matches the given value as key name.
     *
     * @param value
     *            the key name to look for.
     * @return
     *         the {@link PropertyKey} element that matches the given key name value or <code>null</code> if none is found.
     */
    public static PropertyKey fromValue(String value) {
        for (PropertyKey k : values()) {
            if (k.isCaseSensitive) {
                if (k.getKeyName().equals(value) || k.getCcAlias() != null && k.getCcAlias().equals(value)) {
                    return k;
                }
            } else {
                if (k.getKeyName().equalsIgnoreCase(value) || k.getCcAlias() != null && k.getCcAlias().equalsIgnoreCase(value)) {
                    return k;
                }
            }
        }
        return null;
    }

    /**
     * Helper method that normalizes the case of the given key, if it is one of {@link PropertyKey} elements.
     *
     * @param keyName
     *            the key name to normalize.
     * @return
     *         the normalized key name if it belongs to this enum, otherwise returns the input unchanged.
     */
    public static String normalizeCase(String keyName) {
        PropertyKey pk = caseInsensitiveValues.get(keyName);
        return pk == null ? keyName : pk.getKeyName();
        //return keyName;
    }

}
