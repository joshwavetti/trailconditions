terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.0"
    }
  }
}

provider "azurerm" {
  features {}
}

# Resource Group
resource "azurerm_resource_group" "rg" {
  name     = "trailconditions-rg"
  location = "Germany West Central"
}

# Azure Container Registry
resource "azurerm_container_registry" "acr" {
  name                = "trailconditionsacr"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  sku                 = "Basic"
  admin_enabled       = true
}

# AKS Cluster
resource "azurerm_kubernetes_cluster" "aks" {
  name                = "trailconditions-aks"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  dns_prefix          = "trailconditions"

  default_node_pool {
    name       = "default"
    node_count = 1
    vm_size    = "Standard_B2pls_v2"
  }

  identity {
    type = "SystemAssigned"
  }
}

# Allow AKS to pull from ACR
resource "azurerm_role_assignment" "aks_acr" {
  principal_id                     = azurerm_kubernetes_cluster.aks.kubelet_identity[0].object_id
  role_definition_name             = "AcrPull"
  scope                            = azurerm_container_registry.acr.id
  skip_service_principal_aad_check = true
}
