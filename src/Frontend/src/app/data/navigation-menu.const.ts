import { INavigationMenu } from '../interfaces/navigation-menu.interface';

export const NAVIGATION_MENU: INavigationMenu[] = [

    {
        title: '',
        permissions: [],
        links : [
            {
                title: 'Administrador de Documentos',
                icon: 'fas fa-folder-open',
                link: '/auth/document-manager',
                permissions: ['DocumentManager', 'SuperAdmin']
            },
            {
                title: 'Mis Documentos',
                icon: 'fas fa-file-invoice',
                link: '/auth/my-documents',
                permissions: ['User', 'SuperAdmin']
            },
            {
                title: 'Usuarios',
                icon: 'fas fa-users-cog',
                link: '/auth/users',
                permissions: ['SuperAdmin']
            },
            {
                title: 'Logs',
                icon: 'fas fa-cogs',
                link: '/auth/logs',
                permissions: ['SuperAdmin']
            }
        ]
    }
]
