namespace GarageGroup.Infra;

public enum KafkaProduceFailureCode
{
    //
    // Сводка:
    //     Unknown broker error
    Unknown = -1,
    //
    // Сводка:
    //     Success
    NoError = 0,
    //
    // Сводка:
    //     Offset out of range
    OffsetOutOfRange = 1,
    //
    // Сводка:
    //     Invalid message
    InvalidMsg = 2,
    //
    // Сводка:
    //     Unknown topic or partition
    UnknownTopicOrPart = 3,
    //
    // Сводка:
    //     Invalid message size
    InvalidMsgSize = 4,
    //
    // Сводка:
    //     Leader not available
    LeaderNotAvailable = 5,
    //
    // Сводка:
    //     Not leader for partition
    NotLeaderForPartition = 6,
    //
    // Сводка:
    //     Request timed out
    RequestTimedOut = 7,
    //
    // Сводка:
    //     Broker not available
    BrokerNotAvailable = 8,
    //
    // Сводка:
    //     Replica not available
    ReplicaNotAvailable = 9,
    //
    // Сводка:
    //     Message size too large
    MsgSizeTooLarge = 10,
    //
    // Сводка:
    //     StaleControllerEpochCode
    StaleCtrlEpoch = 11,
    //
    // Сводка:
    //     Offset metadata string too large
    OffsetMetadataTooLarge = 12,
    //
    // Сводка:
    //     Broker disconnected before response received
    NetworkException = 13,
    //
    // Сводка:
    //     Group coordinator load in progress
    GroupLoadInProgress = 14,
    //
    // Сводка:
    //     Group coordinator not available
    GroupCoordinatorNotAvailable = 15,
    //
    // Сводка:
    //     Not coordinator for group
    NotCoordinatorForGroup = 16,
    //
    // Сводка:
    //     Invalid topic
    TopicException = 17,
    //
    // Сводка:
    //     Message batch larger than configured server segment size
    RecordListTooLarge = 18,
    //
    // Сводка:
    //     Not enough in-sync replicas
    NotEnoughReplicas = 19,
    //
    // Сводка:
    //     Message(s) written to insufficient number of in-sync replicas
    NotEnoughReplicasAfterAppend = 20,
    //
    // Сводка:
    //     Invalid required acks value
    InvalidRequiredAcks = 21,
    //
    // Сводка:
    //     Specified group generation id is not valid
    IllegalGeneration = 22,
    //
    // Сводка:
    //     Inconsistent group protocol
    InconsistentGroupProtocol = 23,
    //
    // Сводка:
    //     Invalid group.id
    InvalidGroupId = 24,
    //
    // Сводка:
    //     Unknown member
    UnknownMemberId = 25,
    //
    // Сводка:
    //     Invalid session timeout
    InvalidSessionTimeout = 26,
    //
    // Сводка:
    //     Group rebalance in progress
    RebalanceInProgress = 27,
    //
    // Сводка:
    //     Commit offset data size is not valid
    InvalidCommitOffsetSize = 28,
    //
    // Сводка:
    //     Topic authorization failed
    TopicAuthorizationFailed = 29,
    //
    // Сводка:
    //     Group authorization failed
    GroupAuthorizationFailed = 30,
    //
    // Сводка:
    //     Cluster authorization failed
    ClusterAuthorizationFailed = 31,
    //
    // Сводка:
    //     Invalid timestamp
    InvalidTimestamp = 32,
    //
    // Сводка:
    //     Unsupported SASL mechanism
    UnsupportedSaslMechanism = 33,
    //
    // Сводка:
    //     Illegal SASL state
    IllegalSaslState = 34,
    //
    // Сводка:
    //     Unsupported version
    UnsupportedVersion = 35,
    //
    // Сводка:
    //     Topic already exists
    TopicAlreadyExists = 36,
    //
    // Сводка:
    //     Invalid number of partitions
    InvalidPartitions = 37,
    //
    // Сводка:
    //     Invalid replication factor
    InvalidReplicationFactor = 38,
    //
    // Сводка:
    //     Invalid replica assignment
    InvalidReplicaAssignment = 39,
    //
    // Сводка:
    //     Invalid config
    InvalidConfig = 40,
    //
    // Сводка:
    //     Not controller for cluster
    NotController = 41,
    //
    // Сводка:
    //     Invalid request
    InvalidRequest = 42,
    //
    // Сводка:
    //     Message format on broker does not support request
    UnsupportedForMessageFormat = 43,
    //
    // Сводка:
    //     Isolation policy violation
    PolicyViolation = 44,
    //
    // Сводка:
    //     Broker received an out of order sequence number
    OutOfOrderSequenceNumber = 45,
    //
    // Сводка:
    //     Broker received a duplicate sequence number
    DuplicateSequenceNumber = 46,
    //
    // Сводка:
    //     Producer attempted an operation with an old epoch
    InvalidProducerEpoch = 47,
    //
    // Сводка:
    //     Producer attempted a transactional operation in an invalid state
    InvalidTxnState = 48,
    //
    // Сводка:
    //     Producer attempted to use a producer id which is not currently assigned to its
    //     transactional id
    InvalidProducerIdMapping = 49,
    //
    // Сводка:
    //     Transaction timeout is larger than the maximum value allowed by the broker's
    //     max.transaction.timeout.ms
    InvalidTransactionTimeout = 50,
    //
    // Сводка:
    //     Producer attempted to update a transaction while another concurrent operation
    //     on the same transaction was ongoing
    ConcurrentTransactions = 51,
    //
    // Сводка:
    //     Indicates that the transaction coordinator sending a WriteTxnMarker is no longer
    //     the current coordinator for a given producer
    TransactionCoordinatorFenced = 52,
    //
    // Сводка:
    //     Transactional Id authorization failed
    TransactionalIdAuthorizationFailed = 53,
    //
    // Сводка:
    //     Security features are disabled
    SecurityDisabled = 54,
    //
    // Сводка:
    //     Operation not attempted
    OperationNotAttempted = 55,
    //
    // Сводка:
    //     Disk error when trying to access log file on the disk.
    KafkaStorageError = 56,
    //
    // Сводка:
    //     The user-specified log directory is not found in the broker config.
    LogDirNotFound = 57,
    //
    // Сводка:
    //     SASL Authentication failed.
    SaslAuthenticationFailed = 58,
    //
    // Сводка:
    //     Unknown Producer Id.
    UnknownProducerId = 59,
    //
    // Сводка:
    //     Partition reassignment is in progress.
    ReassignmentInProgress = 60,
    //
    // Сводка:
    //     Delegation Token feature is not enabled.
    DelegationTokenAuthDisabled = 61,
    //
    // Сводка:
    //     Delegation Token is not found on server.
    DelegationTokenNotFound = 62,
    //
    // Сводка:
    //     Specified Principal is not valid Owner/Renewer.
    DelegationTokenOwnerMismatch = 63,
    //
    // Сводка:
    //     Delegation Token requests are not allowed on this connection.
    DelegationTokenRequestNotAllowed = 64,
    //
    // Сводка:
    //     Delegation Token authorization failed.
    DelegationTokenAuthorizationFailed = 65,
    //
    // Сводка:
    //     Delegation Token is expired.
    DelegationTokenExpired = 66,
    //
    // Сводка:
    //     Supplied principalType is not supported.
    InvalidPrincipalType = 67,
    //
    // Сводка:
    //     The group is not empty.
    NonEmptyGroup = 68,
    //
    // Сводка:
    //     The group id does not exist.
    GroupIdNotFound = 69,
    //
    // Сводка:
    //     The fetch session ID was not found.
    FetchSessionIdNotFound = 70,
    //
    // Сводка:
    //     The fetch session epoch is invalid.
    InvalidFetchSessionEpoch = 71,
    //
    // Сводка:
    //     No matching listener.
    ListenerNotFound = 72,
    //
    // Сводка:
    //     Topic deletion is disabled.
    TopicDeletionDisabled = 73,
    //
    // Сводка:
    //     Leader epoch is older than broker epoch.
    FencedLeaderEpoch = 74,
    //
    // Сводка:
    //     Leader epoch is newer than broker epoch.
    UnknownLeaderEpoch = 75,
    //
    // Сводка:
    //     Unsupported compression type.
    UnsupportedCompressionType = 76,
    //
    // Сводка:
    //     Broker epoch has changed.
    StaleBrokerEpoch = 77,
    //
    // Сводка:
    //     Leader high watermark is not caught up.
    OffsetNotAvailable = 78,
    //
    // Сводка:
    //     Group member needs a valid member ID.
    MemberIdRequired = 79,
    //
    // Сводка:
    //     Preferred leader was not available.
    PreferredLeaderNotAvailable = 80,
    //
    // Сводка:
    //     Consumer group has reached maximum size.
    GroupMaxSizeReached = 81,
    //
    // Сводка:
    //     Static consumer fenced by other consumer with same group.instance.id.
    FencedInstanceId = 82,
    //
    // Сводка:
    //     Eligible partition leaders are not available.
    EligibleLeadersNotAvailable = 83,
    //
    // Сводка:
    //     Leader election not needed for topic partition.
    ElectionNotNeeded = 84,
    //
    // Сводка:
    //     No partition reassignment is in progress.
    NoReassignmentInProgress = 85,
    //
    // Сводка:
    //     Deleting offsets of a topic while the consumer group is subscribed to it.
    GroupSubscribedToTopic = 86,
    //
    // Сводка:
    //     Broker failed to validate record.
    InvalidRecord = 87,
    //
    // Сводка:
    //     There are unstable offsets that need to be cleared.
    UnstableOffsetCommit = 88,
    //
    // Сводка:
    //     Throttling quota has been exceeded.
    ThrottlingQuotaExceeded = 89,
    //
    // Сводка:
    //     There is a newer producer with the same transactionalId which fences the current
    //     one.
    ProducerFenced = 90,
    //
    // Сводка:
    //     Request illegally referred to resource that does not exist.
    ResourceNotFound = 91,
    //
    // Сводка:
    //     Request illegally referred to the same resource twice.
    DuplicateResource = 92,
    //
    // Сводка:
    //     Requested credential would not meet criteria for acceptability.
    UnacceptableCredential = 93,
    //
    // Сводка:
    //     Indicates that the either the sender or recipient of a voter-only request is
    //     not one of the expected voters.
    InconsistentVoterSet = 94,
    //
    // Сводка:
    //     Invalid update version.
    InvalidUpdateVersion = 95,
    //
    // Сводка:
    //     Unable to update finalized features due to server error.
    FeatureUpdateFailed = 96,
    //
    // Сводка:
    //     Request principal deserialization failed during forwarding.
    PrincipalDeserializationFailure = 97,
    //
    // Сводка:
    //     Unknown Topic Id.
    UnknownTopicId = 100,
    //
    // Сводка:
    //     The member epoch is fenced by the group coordinator.
    FencedMemberEpoch = 110,
    //
    // Сводка:
    //     The instance ID is still used by another member in the consumer group.
    UnreleasedInstanceId = 111,
    //
    // Сводка:
    //     The assignor or its version range is not supported by the consumer group.
    UnsupportedAssignor = 112,
    //
    // Сводка:
    //     The member epoch is stale.
    StaleMemberEpoch = 113,
    //
    // Сводка:
    //     Client sent a push telemetry request with an invalid or outdated subscription
    //     ID.
    UnknownSubscriptionId = 117,
    //
    // Сводка:
    //     Client sent a push telemetry request larger than the maximum size the broker
    //     will accept.
    TelemetryTooLarge = 118
}