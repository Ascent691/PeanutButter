﻿Imports System.Data.Common
Imports NSubstitute

Public Class BuilderFaker
    Public Shared Function CreateFakeSelectStatementBuilder(Optional withBuildSql As String = Nothing) As ISelectStatementBuilder
        Dim builder = Substitute.For(Of ISelectStatementBuilder)()
        builder.Distinct().Returns(builder)
        builder.WithTable(Arg.Any(Of String)).Returns(builder)
        builder.WithField(Arg.Any(Of String), Arg.Any(Of String)()).Returns(builder)
        builder.WithField(Arg.Any(Of SelectField)).Returns(builder)
        builder.WithCondition(Arg.Any(Of String)).Returns(builder)
        builder.WithCondition(Arg.Any(Of String)(), Arg.Any(Of Condition.EqualityOperators)(), Arg.Any(Of DateTime)).Returns(builder)
        builder.WithCondition(Arg.Any(Of String)(), Arg.Any(Of Condition.EqualityOperators)(), Arg.Any(Of Int16)).Returns(builder)
        builder.WithCondition(Arg.Any(Of String)(), Arg.Any(Of Condition.EqualityOperators)(), Arg.Any(Of Int32)).Returns(builder)
        builder.WithCondition(Arg.Any(Of String)(), Arg.Any(Of Condition.EqualityOperators)(), Arg.Any(Of Int64)).Returns(builder)
        builder.WithCondition(Arg.Any(Of String)(), Arg.Any(Of Condition.EqualityOperators)(), Arg.Any(Of Long)).Returns(builder)
        builder.WithCondition(Arg.Any(Of String)(), Arg.Any(Of Condition.EqualityOperators)(), Arg.Any(Of String)).Returns(builder)
        builder.WithCondition(Arg.Any(Of String)(), Arg.Any(Of Condition.EqualityOperators)(), Arg.Any(Of Decimal)()).Returns(builder)
        builder.WithCondition(Arg.Any(Of SelectField)(), Arg.Any(Of Condition.EqualityOperators)(), Arg.Any(Of String)()).Returns(builder)
        builder.WithCondition(Arg.Any(Of SelectField)(), Arg.Any(Of Condition.EqualityOperators)(), Arg.Any(Of Int16)()).Returns(builder)
        builder.WithCondition(Arg.Any(Of SelectField)(), Arg.Any(Of Condition.EqualityOperators)(), Arg.Any(Of Int32)()).Returns(builder)
        builder.WithCondition(Arg.Any(Of SelectField)(), Arg.Any(Of Condition.EqualityOperators)(), Arg.Any(Of Int64)()).Returns(builder)
        builder.WithCondition(Arg.Any(Of SelectField)(), Arg.Any(Of Condition.EqualityOperators)(), Arg.Any(Of Long)()).Returns(builder)
        builder.WithCondition(Arg.Any(Of SelectField)(), Arg.Any(Of Condition.EqualityOperators)(), Arg.Any(Of Decimal)()).Returns(builder)
        builder.WithCondition(Arg.Any(Of SelectField)(), Arg.Any(Of Condition.EqualityOperators)(), Arg.Any(Of Double)()).Returns(builder)
        builder.WithCondition(Arg.Any(Of SelectField)(), Arg.Any(Of Condition.EqualityOperators)(), Arg.Any(Of DateTime)()).Returns(builder)
        builder.WithCondition(Arg.Any(Of SelectField)(), Arg.Any(Of Condition.EqualityOperators)(), Arg.Any(Of SelectField)()).Returns(builder)
        builder.WithInnerJoin(Arg.Any(Of String)(), Arg.Any(Of String)(), Arg.Any(Of String)(), Arg.Any(Of String)()).Returns(builder)
        builder.WithComputedField(Arg.Any(Of String)(), Arg.Any(Of ComputedField.ComputeFunctions)(), Arg.Any(Of String)()).Returns(builder)
        builder.WithAllFieldsFrom(Arg.Any(Of String)()).Returns(builder)
        builder.WithAllConditions(Arg.Any(Of Condition)()).Returns(builder)
        builder.WithAllConditions(Arg.Any(Of Condition)(), Arg.Any(Of Condition)()).Returns(builder)
        If Not withBuildSql Is Nothing Then
            builder.Build.Returns(withBuildSql)
        End If
        Return builder
    End Function

    Public Shared Function CreateFakeUpdateStatementBuilder(Optional withBuildSql As String = Nothing) As IUpdateStatementBuilder
        Dim builder = Substitute.For(Of IUpdateStatementBuilder)()
        builder.WithTable(Arg.Any(Of String)()).Returns(builder)
        builder.WithField(Arg.Any(Of String)(), Arg.Any(Of String)()).Returns(builder)
        builder.WithField(Arg.Any(Of String)(), Arg.Any(Of Decimal)(), Arg.Any(Of String)()).Returns(builder)
        builder.WithField(Arg.Any(Of String)(), Arg.Any(Of Nullable(Of Decimal))(), Arg.Any(Of String)()).Returns(builder)
        builder.WithField(Arg.Any(Of String)(), Arg.Any(Of DateTime)(), Arg.Any(Of String)()).Returns(builder)
        builder.WithField(Arg.Any(Of String)(), Arg.Any(Of Nullable(Of DateTime))(), Arg.Any(Of String)()).Returns(builder)
        builder.WithField(Arg.Any(Of String)(), Arg.Any(Of Int64)()).Returns(builder)
        builder.WithField(Arg.Any(Of String)(), Arg.Any(Of Int32)()).Returns(builder)
        builder.WithField(Arg.Any(Of String)(), Arg.Any(Of Int16)()).Returns(builder)
        builder.WithNullField(Arg.Any(Of String)()).Returns(builder)
        builder.WithCondition(Arg.Any(Of String)(), Arg.Any(Of Condition.EqualityOperators)(), Arg.Any(Of Int32)()).Returns(builder)
        builder.WithCondition(Arg.Any(Of String)).Returns(builder)
        builder.WithAllConditions(Arg.Any(Of ICondition), Arg.Any(Of ICondition)()).Returns(builder)
        If Not withBuildSql Is Nothing Then
            builder.Build.Returns(withBuildSql)
        End If
        Return builder
    End Function

    Public Shared Function CreateFakeInsertStatementBuilder(Optional withBuildSql As String = Nothing) As IInsertStatementBuilder
        Dim builder = Substitute.For(Of IInsertStatementBuilder)()
        builder.WithTable(Arg.Any(Of String)()).Returns(builder)
        builder.WithField(Arg.Any(Of String)(), Arg.Any(Of String)()).Returns(builder)
        builder.WithField(Arg.Any(Of String)(), Arg.Any(Of Decimal)(), Arg.Any(Of String)()).Returns(builder)
        builder.WithField(Arg.Any(Of String)(), Arg.Any(Of Nullable(Of Decimal))(), Arg.Any(Of String)()).Returns(builder)
        builder.WithField(Arg.Any(Of String)(), Arg.Any(Of DateTime)()).Returns(builder)
        builder.WithField(Arg.Any(Of String)(), Arg.Any(Of Nullable(Of DateTime))()).Returns(builder)
        builder.WithField(Arg.Any(Of String)(), Arg.Any(Of Int64)()).Returns(builder)
        builder.WithField(Arg.Any(Of String)(), Arg.Any(Of Int32)()).Returns(builder)
        builder.WithField(Arg.Any(Of String)(), Arg.Any(Of Int16)()).Returns(builder)
        If Not withBuildSql Is Nothing Then
            builder.Build.Returns(withBuildSql)
        End If
        Return builder
    End Function

    Public Shared Function CreateFakeDataReaderBuilder(Optional withBuildDataReader As DbDataReader = Nothing) As IDataReaderBuilder
        Dim builder = Substitute.For(Of IDataReaderBuilder)()
        builder.WithSql(Arg.Any(Of String)()).Returns(builder)
        builder.WithSql(Arg.Any(Of ISelectStatementBuilder)()).Returns(builder)
        builder.WithConnectionFactory(Arg.Any(Of Func(Of IDbConnection))()).Returns(builder)
        If withBuildDataReader Is Nothing Then Return builder
        builder.Build().Returns(withBuildDataReader)
        Return builder
    End Function

    Public Shared Function CreateFakeDataReaderBuilder(ParamArray withReaderReadResult() As Boolean) As IDataReaderBuilder
        Dim reader = Substitute.For(Of DbDataReader)()
        Dim results = New Queue(Of Boolean)
        For Each r In withReaderReadResult
            results.Enqueue(r)
        Next
        Dim alwaysReturn As Boolean? = Nothing
        If results.Count() = 1 Then
            alwaysReturn = results.Dequeue()
        End If
        reader.Read().ReturnsForAnyArgs(Function(args)
                                            If results.Count() = 0 Then
                                                If alwaysReturn.HasValue Then
                                                    Return alwaysReturn.Value
                                                End If
                                                Return False
                                            End If
                                            Return results.Dequeue()
                                        End Function)
        Return CreateFakeDataReaderBuilder(reader)
    End Function

    Public Shared Function CreateFakeScalarExecutorBuilder(Optional withConnection As IDbConnection = Nothing) As IScalarExecutorBuilder
        Dim builder = Substitute.For(Of IScalarExecutorBuilder)()
        builder.WithConnectionFactory(Arg.Any(Of Func(Of IDbConnection))()).Returns(builder)
        builder.WithSql(Arg.Any(Of String)()).Returns(builder)
        builder.WithSql(Arg.Any(Of IInsertStatementBuilder)()).Returns(builder)
        builder.WithSql(Arg.Any(Of IUpdateStatementBuilder)()).Returns(builder)
        ' TODO: mock out returning something from Execute & ExecuteInsert
        Return builder
    End Function

End Class
