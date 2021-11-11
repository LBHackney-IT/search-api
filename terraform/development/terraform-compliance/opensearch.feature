Feature: OpenSearch is used to host the ElasticSearch clusters
  In order to improve security
  As engineers
  We'll use ensure our OpenSearch clusters are configured correctly

  Scenario: Ensure it is deployed in a VPC
    Given I have aws_elasticsearch_domain defined
    Then it must contain vpc_options

  Scenario: Ensure OpenSearch clusters are encrypted at rest
    Given I have aws_elasticsearch_domain defined
    Then it must contain encrypt_at_rest
    And its value must be "enabled = true"
