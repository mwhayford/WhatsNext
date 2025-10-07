import { useAuth } from '../../contexts/AuthContext';

export const Dashboard = () => {
  const { user } = useAuth();

  return (
    <div className="space-y-6">
      <div className="bg-white rounded-lg shadow p-6">
        <h2 className="text-2xl font-bold text-gray-900 mb-4">
          Welcome to WhatsNext! 🎉
        </h2>
        <p className="text-gray-600 mb-6">
          Your personal productivity dashboard is ready. Here's what you can do:
        </p>
        
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          <div className="border border-gray-200 rounded-lg p-4 hover:border-primary-500 transition-colors">
            <div className="flex items-center space-x-3 mb-2">
              <span className="text-2xl">✅</span>
              <h3 className="font-semibold text-gray-900">Habit Tracker</h3>
            </div>
            <p className="text-sm text-gray-600">
              Track your daily habits and build streaks
            </p>
            <span className="inline-block mt-3 text-xs text-gray-500 bg-gray-100 px-2 py-1 rounded">
              Coming Soon
            </span>
          </div>

          <div className="border border-gray-200 rounded-lg p-4 hover:border-primary-500 transition-colors">
            <div className="flex items-center space-x-3 mb-2">
              <span className="text-2xl">📝</span>
              <h3 className="font-semibold text-gray-900">Task Manager</h3>
            </div>
            <p className="text-sm text-gray-600">
              Organize and prioritize your tasks
            </p>
            <span className="inline-block mt-3 text-xs text-gray-500 bg-gray-100 px-2 py-1 rounded">
              Coming Soon
            </span>
          </div>

          <div className="border border-gray-200 rounded-lg p-4 hover:border-primary-500 transition-colors">
            <div className="flex items-center space-x-3 mb-2">
              <span className="text-2xl">⏱️</span>
              <h3 className="font-semibold text-gray-900">Pomodoro Timer</h3>
            </div>
            <p className="text-sm text-gray-600">
              Focus with timed work sessions
            </p>
            <span className="inline-block mt-3 text-xs text-gray-500 bg-gray-100 px-2 py-1 rounded">
              Coming Soon
            </span>
          </div>
        </div>
      </div>

      <div className="bg-gradient-to-r from-primary-500 to-primary-700 rounded-lg shadow p-6 text-white">
        <h3 className="text-xl font-bold mb-2">Your Account</h3>
        <div className="space-y-1">
          <p>
            <span className="opacity-90">Username:</span>{' '}
            <span className="font-semibold">{user?.username}</span>
          </p>
          <p>
            <span className="opacity-90">Email:</span>{' '}
            <span className="font-semibold">{user?.email}</span>
          </p>
          <p>
            <span className="opacity-90">User ID:</span>{' '}
            <span className="font-semibold">{user?.userId}</span>
          </p>
          {user?.roles && user.roles.length > 0 && (
            <p>
              <span className="opacity-90">Roles:</span>{' '}
              <span className="font-semibold">{user.roles.join(', ')}</span>
            </p>
          )}
        </div>
      </div>

      <div className="bg-blue-50 border border-blue-200 rounded-lg p-6">
        <h3 className="text-lg font-semibold text-blue-900 mb-2">
          🚀 Successfully Authenticated!
        </h3>
        <p className="text-blue-800">
          Your authentication is working perfectly. The JWT token is being stored 
          and sent with each API request. Features will be added soon!
        </p>
      </div>
    </div>
  );
};

