// src/App.tsx
import { useState, useEffect } from 'react'
import { TareaGlobalCard, type TareaGlobalProps } from './components/TareaGlobalCard'

function App() {
    // =========================================================================
    // ZONA 3: EL CEREBRO (CONEXIÓN AL ALMACÉN GLOBAL DE REACT)
    // =========================================================================

    // Casillero #0: arreglo de tareas, tipado con el contrato exportado
    const [tareas, setTareas] = useState<TareaGlobalProps[]>([])

    // Casillero #1: controla el cartel de espera
    const [cargando, setCargando] = useState<boolean>(true)

    // Casillero #2: texto del buscador
    const [busqueda, setBusqueda] = useState<string>('')

    // useEffect con [] se ejecuta UNA SOLA VEZ tras el montaje (Vuelta 1)
    useEffect(() => {
        // Simulamos la latencia de red (1.2 seg) de una consulta al servidor
        const temporizador = setTimeout(() => {
            const datosSimuladosBD: TareaGlobalProps[] = [
                { id: 1, nombre: 'Entregar informe de programación web', descripcion: 'Subir el documento en PDF a la plataforma antes del viernes.', fechaCreacion: '2026-10-28T09:00:00+00:00', completada: false },
                { id: 2, nombre: 'Estudiar para el examen parcial', descripcion: 'Repasar las unidades 1 y 2 de la clase.', fechaCreacion: '2026-10-29T15:30:00+00:00', completada: false },
                { id: 3, nombre: 'Revisar correos pendientes', descripcion: 'Responder los mensajes de los docentes.', fechaCreacion: '2026-10-30T08:15:00+00:00', completada: true },
                { id: 4, nombre: 'Practicar algoritmos en C# durante 45 minutos', descripcion: 'Resolver ejercicios de listas y diccionarios.', fechaCreacion: '2026-10-30T10:00:00+00:00', completada: true },
                { id: 5, nombre: 'Documentar avances del sprint en el repositorio', descripcion: 'Actualizar el README y el tablero de GitHub Projects.', fechaCreacion: '2026-10-30T11:20:00+00:00', completada: false },
                { id: 6, nombre: 'Subir los avances del frontend al repositorio', descripcion: 'Hacer commit y push de los cambios de la Clase 13.', fechaCreacion: '2026-10-30T12:45:00+00:00', completada: false },
            ]

            // Guarda en el almacén y dispara la Vuelta 2 (re-render)
            setTareas(datosSimuladosBD)
            setCargando(false)
        }, 1200)

        // Limpieza si el componente se desmonta antes de que termine el tiempo
        return () => clearTimeout(temporizador)
    }, [])

    // Se recalcula en cada refresco con los datos re-inyectados
    const tareasFiltradas = tareas.filter((tarea) =>
        tarea.nombre.toLowerCase().includes(busqueda.toLowerCase())
    )

    // =========================================================================
    // ZONA 4: EL RENDERIZADO DECLARATIVO
    // =========================================================================
    return (
        <div className="min-h-screen bg-slate-900 text-slate-100 flex flex-col md:flex-row">

            {/* 1. SIDEBAR */}
            <aside className="hidden md:flex md:w-64 md:flex-col bg-slate-950 border-r border-slate-800 justify-between p-5 shrink-0">
                <div>
                    <div className="flex items-center gap-3 mb-8 px-2">
                        <div className="w-8 h-8 rounded-lg bg-indigo-600 flex items-center justify-center font-bold text-white shadow">
                            AF
                        </div>
                        <span className="text-xl font-bold tracking-tight text-white">AgendaForge</span>
                    </div>

                    <nav className="space-y-2">
                        <a href="#" className="flex items-center justify-between px-3 py-2.5 bg-indigo-600/15 text-indigo-400 rounded-lg font-medium text-sm border border-indigo-500/20">
                            <span className="flex items-center gap-3"><span>🌐</span> Catálogo Global</span>
                            {/* Pasa de 0 (Vuelta 1) a 6 (Vuelta 2) */}
                            <span className="bg-indigo-500/20 text-indigo-300 text-xs px-2 py-0.5 rounded-full">
                {tareas.length}
              </span>
                        </a>
                        <a href="#" className="flex items-center gap-3 px-3 py-2.5 text-slate-500 rounded-lg font-medium text-sm cursor-not-allowed">
                            <span>📋</span> Mis Tareas (Unidad 3)
                        </a>
                        <a href="#" className="flex items-center gap-3 px-3 py-2.5 text-slate-500 rounded-lg font-medium text-sm cursor-not-allowed">
                            <span>📊</span> Progreso (Unidad 3)
                        </a>
                    </nav>
                </div>

                <div className="p-3.5 bg-slate-900 rounded-xl border border-slate-800">
                    <p className="text-xs text-slate-400 mb-1">Estado de Seguridad</p>
                    <p className="text-sm font-semibold text-amber-400">👤 Modo Invitado</p>
                </div>
            </aside>

            {/* 2. CABECERA MÓVIL */}
            <header className="md:hidden bg-slate-950 border-b border-slate-800 p-4 flex items-center justify-between">
                <div className="flex items-center gap-2.5">
                    <div className="w-7 h-7 rounded-md bg-indigo-600 flex items-center justify-center font-bold text-sm text-white">
                        AF
                    </div>
                    <span className="font-bold text-lg text-white">AgendaForge</span>
                </div>
                <span className="text-xs bg-amber-500/10 text-amber-400 px-2.5 py-1 rounded-md border border-amber-500/20 font-medium">
          👤 Invitado
        </span>
            </header>

            {/* 3. WORKSPACE */}
            <main className="flex-1 p-6 md:p-10 overflow-y-auto">

                <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4 pb-6 mb-6 border-b border-slate-800">
                    <div>
                        <h1 className="text-2xl md:text-3xl font-bold text-white">Catálogo Global de Tareas</h1>
                        <p className="text-slate-400 text-sm mt-1">
                            Explora las tareas registradas en la tabla <code className="text-indigo-400 font-mono">TareasGlobales</code>.
                        </p>
                    </div>

                    <button className="bg-indigo-600 hover:bg-indigo-500 text-white font-medium text-sm px-4 py-2.5 rounded-lg transition-colors shadow-sm self-start sm:self-auto cursor-pointer">
                        + Nueva Tarea Global
                    </button>
                </div>

                {/* Buscador reactivo: cada tecla ejecuta setBusqueda() y refresca App() */}
                <div className="mb-6 flex flex-col sm:flex-row sm:items-center justify-between gap-4">
                    <input
                        type="text"
                        placeholder="🔍 Filtrar tareas en el catálogo..."
                        value={busqueda}
                        onChange={(e) => setBusqueda(e.target.value)}
                        className="w-full sm:max-w-md bg-slate-950 border border-slate-800 rounded-lg px-4 py-2.5 text-sm text-white placeholder-slate-500 focus:outline-none focus:border-indigo-500 transition-colors"
                    />
                    <span className="text-xs text-slate-400">
            Mostrando <strong className="text-white">{tareasFiltradas.length}</strong> de {tareas.length} registros en memoria
          </span>
                </div>

                {/* RENDERIZADO CONDICIONAL: Vuelta 1 (cargando) vs Vuelta 2 (datos) */}
                {cargando ? (
                    <div className="bg-slate-800/50 border border-slate-800 rounded-xl p-12 text-center">
                        <p className="text-indigo-400 font-semibold animate-pulse text-lg">
                            ⏳ Sincronizando catálogo desde el Almacén de Estado...
                        </p>
                        <p className="text-slate-500 text-xs mt-2">
                            Ejecutando Vuelta 1 (Montaje inicial y disparo de useEffect)
                        </p>
                    </div>
                ) : (
                    <section className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-5">
                        {tareasFiltradas.map((tarea) => (
                            <TareaGlobalCard
                                key={tarea.id}
                                id={tarea.id}
                                nombre={tarea.nombre}
                                descripcion={tarea.descripcion}
                                fechaCreacion={tarea.fechaCreacion}
                                completada={tarea.completada}
                            />
                        ))}
                    </section>
                )}

            </main>
        </div>
    )
}

export default App