#!/bin/bash
# Infrastructure/Database/run-migrations.sh

set -e  # Detener si hay algún error

echo "🔄 Esperando a que PostgreSQL esté listo..."

# Intentar conectar hasta que PostgreSQL responda
until PGPASSWORD=$POSTGRES_PASSWORD psql -h "$POSTGRES_HOST" -U "$POSTGRES_USER" -d "$POSTGRES_DB" -c '\q' 2>/dev/null; do
  echo "⏳ PostgreSQL no está listo - esperando..."
  sleep 2
done

echo "✅ PostgreSQL está listo!"
echo ""
echo "📝 Ejecutando scripts de migración..."
echo "=================================="

# Exportar password para psql
export PGPASSWORD=$POSTGRES_PASSWORD

# Ejecutar scripts en orden alfabético
for script in $(ls /scripts/*.sql | sort); do
    script_name=$(basename "$script")
    echo ""
    echo "▶️  Ejecutando: $script_name"
    
    if psql -h "$POSTGRES_HOST" -U "$POSTGRES_USER" -d "$POSTGRES_DB" -f "$script"; then
        echo "   ✅ $script_name completado"
    else
        echo "   ❌ Error en $script_name"
        exit 1
    fi
done

echo ""
echo "🎉 ¡Todas las migraciones completadas exitosamente!"
