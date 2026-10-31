// src/components/TareaGlobalCard.tsx

// --- ZONA 1: EL CONTRATO (INTERFAZ) ---
export interface TareaGlobalProps {
    id: number;
    nombre: string;
    descripcion: string;
    fechaCreacion: string;
    completada: boolean;
}

// --- ZONA 2: LA FIRMA ---
export function TareaGlobalCard({ id, nombre, descripcion, fechaCreacion, completada }: TareaGlobalProps) {

    // --- ZONA 3: EL CEREBRO (LÓGICA) ---
    const fechaFormateada = new Date(fechaCreacion).toLocaleDateString('es-HN', {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
    });

    // --- ZONA 4: EL RENDERIZADO (APLICANDO DESIGN TOKENS) ---
    return (
        <article className="bg-slate-800 border border-slate-700 rounded-xl p-5 flex flex-col justify-between hover:border-indigo-500 transition-colors shadow-sm">
            <div>
                {/* Cabecera: badge con ID + estado */}
                <div className="flex items-center justify-between mb-3">
                    <span className="bg-indigo-500/10 text-indigo-400 text-xs font-semibold px-2.5 py-1 rounded-md border border-indigo-500/20">
                        ID Catálogo #{id}
                    </span>
                    {completada ? (
                        <span className="bg-emerald-500/10 text-emerald-400 text-xs font-semibold px-2.5 py-1 rounded-md border border-emerald-500/20">
                            ✅ Completada
                        </span>
                    ) : (
                        <span className="bg-amber-500/10 text-amber-400 text-xs font-semibold px-2.5 py-1 rounded-md border border-amber-500/20">
                            ⏳ Pendiente
                        </span>
                    )}
                </div>

                <h3 className="text-lg font-bold text-white mb-2">{nombre}</h3>
                <p className="text-sm text-slate-400 mb-4">{descripcion}</p>
            </div>

            <div>
                <p className="text-xs text-slate-500 mb-4">Creada el {fechaFormateada}</p>

                {/* Se habilitará cuando implementemos Usuarios en la Unidad 3 */}
                <button
                    disabled
                    className="w-full bg-slate-900/80 text-slate-400 text-sm font-medium py-2.5 px-4 rounded-lg cursor-not-allowed border border-slate-700"
                >
                    🔒 Marcar como completada (Requiere Sesión)
                </button>
            </div>
        </article>
    );
}